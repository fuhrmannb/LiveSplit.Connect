using System;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class ConnectComponentSettings : UserControl
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public bool AutoStart { get; set; }

        public ConnectComponentSettings()
        {
            InitializeComponent();

            Host = "localhost";
            Port = 7592;
            AutoStart = false;

            bindTextBox.DataBindings.Add("Text", this, "Host", false, DataSourceUpdateMode.OnPropertyChanged);
            portNumericUpDown.DataBindings.Add("Value", this, "Port", false, DataSourceUpdateMode.OnPropertyChanged);
            autoStartCheckBox.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);

            return parent;
        }

        public void SetSettings(XmlNode settings)
        {
            Host = SettingsHelper.ParseString(settings["Host"]);
            Port = (ushort)SettingsHelper.ParseInt(settings["Port"]);
            AutoStart = SettingsHelper.ParseBool(settings["AutoStart"]);
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return HashCode.Combine(
                SettingsHelper.CreateSetting(document, parent, "Host", Host),
                SettingsHelper.CreateSetting<int>(document, parent, "Port", Port),
                SettingsHelper.CreateSetting(document, parent, "AutoStart", AutoStart)
            );
        }
    }
}