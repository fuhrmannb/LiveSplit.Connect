using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Grpc.Core;
using LiveSplit.Connect;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    public class ConnectComponent : LogicComponent
    {
        public const int ServerStartMaxRetry = 10;
        public readonly TimeSpan ServerStartWaitBeforeRetry = TimeSpan.FromSeconds(0.5);

        public override string ComponentName => "LiveSplit Connect";
        public ConnectComponentSettings Settings { get; set; }

        private readonly LiveSplitState state;
        private Grpc.Core.Server gRPCServer;
        private bool serverStarted;
        private bool firstSetSettings;

        public ConnectComponent(LiveSplitState state)
        {
            this.state = state;
            Settings = new ConnectComponentSettings();
            serverStarted = false;
            firstSetSettings = true;
            ContextMenuControls = new Dictionary<string, Action>();
        }

        private void RefreshContextMenu()
        {
            ContextMenuControls.Clear();
            if (serverStarted)
            {
                ContextMenuControls.Add("Stop Connect server", StopServer);
            }
            else
            {
                ContextMenuControls.Add("Start Connect server", StartServer);
            }
        }

        public async void StartServer()
        {
            if (serverStarted) return;

            Trace.WriteLine("Starting LiveSplit.Connect server...");

            // Try to start the server multiple times
            // Useful when reloading the layout, new component does not wait previous one to be disposed.
            var tries = 0;
            IOException lastException = new IOException();
            while (!serverStarted && tries < ServerStartMaxRetry)
            {
                gRPCServer = new Grpc.Core.Server
                {
                    Services = { Connect.LiveSplitService.BindService(new ConnectGRPCServer(state, Settings.ReadOnly)) },
                    Ports = { new ServerPort(Settings.Host, Settings.Port, ServerCredentials.Insecure) },
                };
                try
                {
                    gRPCServer.Start();
                    serverStarted = true;
                }
                catch (IOException e)
                {
                    Trace.WriteLine($"Failed to bind port, retrying in {ServerStartWaitBeforeRetry.TotalMilliseconds}ms ...");
                    await Task.Delay(ServerStartWaitBeforeRetry);
                    tries++;
                    lastException = e;
                }
            }
            if (tries == ServerStartMaxRetry)
            {
                throw lastException;
            }

            RefreshContextMenu();
            Trace.WriteLine($"LiveSplit.Connect server start on {Settings.Host}:{Settings.Port}");
        }

        public void StopServer()
        {
            if (!serverStarted) return;

            Trace.WriteLine("Stopping LiveSplit.Connect server...");
            gRPCServer.KillAsync().Wait();
            serverStarted = false;

            RefreshContextMenu();
            Trace.WriteLine("LiveSplit.Connect server stopped");
        }

        public override void Dispose()
        {
            StopServer();
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public int GetSettingsHashCode()
        {
            return Settings.GetSettingsHashCode();
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);

            // First SetSettings is done when initialising the component.
            // In that case, check here if we should auto-start the gRPC server.
            if (firstSetSettings)
            {
                if (Settings.AutoStart)
                {
                    StartServer();
                }
                else
                {
                    RefreshContextMenu();
                }
                firstSetSettings = false;
            }
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
        }
    }
}
