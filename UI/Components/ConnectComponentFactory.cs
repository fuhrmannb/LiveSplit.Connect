using System;
using LiveSplit.Model;
using LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(ConnectComponentFactory))]

namespace LiveSplit.UI.Components
{
    public class ConnectComponentFactory : IComponentFactory
    {
        public string ComponentName => "LiveSplit Connect";

        public string Description => "Connect to LiveSplit through differents APIs (gRPC).";

        public ComponentCategory Category => ComponentCategory.Control;

        public IComponent Create(LiveSplitState state) => new ConnectComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "TODO/update.LiveSplit.Discord.xml";

        public string UpdateURL => "https://github.com/fuhrmannb/LiveSplit.Connect/releases";

        public Version Version => Version.Parse("0.1.0");
    }
}