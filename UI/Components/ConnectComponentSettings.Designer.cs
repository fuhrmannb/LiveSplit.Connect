
namespace LiveSplit.UI.Components
{
    partial class ConnectComponentSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.settingLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.bindLabel = new System.Windows.Forms.Label();
            this.bindTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.autoStartLabel = new System.Windows.Forms.Label();
            this.autoStartCheckBox = new System.Windows.Forms.CheckBox();
            this.connectComponentSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.readOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.readOnlyLabel = new System.Windows.Forms.Label();
            this.settingLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectComponentSettingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // settingLayoutPanel
            // 
            this.settingLayoutPanel.ColumnCount = 2;
            this.settingLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.settingLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.settingLayoutPanel.Controls.Add(this.readOnlyLabel, 0, 3);
            this.settingLayoutPanel.Controls.Add(this.readOnlyCheckBox, 0, 3);
            this.settingLayoutPanel.Controls.Add(this.bindLabel, 0, 0);
            this.settingLayoutPanel.Controls.Add(this.bindTextBox, 1, 0);
            this.settingLayoutPanel.Controls.Add(this.portLabel, 0, 1);
            this.settingLayoutPanel.Controls.Add(this.portNumericUpDown, 1, 1);
            this.settingLayoutPanel.Controls.Add(this.autoStartLabel, 0, 2);
            this.settingLayoutPanel.Controls.Add(this.autoStartCheckBox, 1, 2);
            this.settingLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.settingLayoutPanel.Name = "settingLayoutPanel";
            this.settingLayoutPanel.RowCount = 4;
            this.settingLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.settingLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.settingLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.settingLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.settingLayoutPanel.Size = new System.Drawing.Size(230, 96);
            this.settingLayoutPanel.TabIndex = 0;
            // 
            // bindLabel
            // 
            this.bindLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bindLabel.AutoSize = true;
            this.bindLabel.Location = new System.Drawing.Point(3, 6);
            this.bindLabel.Name = "bindLabel";
            this.bindLabel.Size = new System.Drawing.Size(69, 13);
            this.bindLabel.TabIndex = 0;
            this.bindLabel.Text = "Bind Address";
            // 
            // bindTextBox
            // 
            this.bindTextBox.Location = new System.Drawing.Point(120, 3);
            this.bindTextBox.Name = "bindTextBox";
            this.bindTextBox.Size = new System.Drawing.Size(99, 20);
            this.bindTextBox.TabIndex = 2;
            // 
            // portLabel
            // 
            this.portLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(3, 32);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 1;
            this.portLabel.Text = "Port";
            // 
            // portNumericUpDown
            // 
            this.portNumericUpDown.Location = new System.Drawing.Point(120, 29);
            this.portNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumericUpDown.Name = "portNumericUpDown";
            this.portNumericUpDown.Size = new System.Drawing.Size(99, 20);
            this.portNumericUpDown.TabIndex = 3;
            // 
            // autoStartLabel
            // 
            this.autoStartLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autoStartLabel.AutoSize = true;
            this.autoStartLabel.Location = new System.Drawing.Point(3, 55);
            this.autoStartLabel.Name = "autoStartLabel";
            this.autoStartLabel.Size = new System.Drawing.Size(111, 13);
            this.autoStartLabel.TabIndex = 4;
            this.autoStartLabel.Text = "Start server on launch";
            // 
            // autoStartCheckBox
            // 
            this.autoStartCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autoStartCheckBox.AutoSize = true;
            this.autoStartCheckBox.Location = new System.Drawing.Point(120, 55);
            this.autoStartCheckBox.Name = "autoStartCheckBox";
            this.autoStartCheckBox.Size = new System.Drawing.Size(15, 14);
            this.autoStartCheckBox.TabIndex = 5;
            this.autoStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // readOnlyCheckBox
            // 
            this.readOnlyCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.readOnlyCheckBox.AutoSize = true;
            this.readOnlyCheckBox.Location = new System.Drawing.Point(120, 77);
            this.readOnlyCheckBox.Name = "readOnlyCheckBox";
            this.readOnlyCheckBox.Size = new System.Drawing.Size(15, 14);
            this.readOnlyCheckBox.TabIndex = 6;
            this.readOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // readOnlyLabel
            // 
            this.readOnlyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.readOnlyLabel.AutoSize = true;
            this.readOnlyLabel.Location = new System.Drawing.Point(3, 77);
            this.readOnlyLabel.Name = "readOnlyLabel";
            this.readOnlyLabel.Size = new System.Drawing.Size(57, 13);
            this.readOnlyLabel.TabIndex = 7;
            this.readOnlyLabel.Text = "Read Only";
            // 
            // ConnectComponentSettings
            // 
            this.Controls.Add(this.settingLayoutPanel);
            this.Name = "ConnectComponentSettings";
            this.Size = new System.Drawing.Size(389, 264);
            this.settingLayoutPanel.ResumeLayout(false);
            this.settingLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectComponentSettingsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel settingLayoutPanel;
        private System.Windows.Forms.Label bindLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox bindTextBox;
        private System.Windows.Forms.BindingSource connectComponentSettingsBindingSource;
        private System.Windows.Forms.NumericUpDown portNumericUpDown;
        private System.Windows.Forms.Label autoStartLabel;
        private System.Windows.Forms.CheckBox autoStartCheckBox;
        private System.Windows.Forms.CheckBox readOnlyCheckBox;
        private System.Windows.Forms.Label readOnlyLabel;
    }
}
