using NgrokUI.Utility;
using NgrokUI.API;
using NgrokUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgrokUI
{
    public class MainWindowModel
    {
        public EventHandler<Utility.EventArgs> elaborationCompleted;
        public bool StartTunnelResult;
        public Tunnel TunnelResult;
        private HttpRequest _httpRequest;

        public MainWindowModel()
        {
            _httpRequest = new HttpRequest();
        }

        public void StartTunnel(EnumDictionary.Protocols protocol, int port)
        {
            var task = new Task(() =>
            {
                StartTunnelResult = _httpRequest.StartTunnel(port, protocol);
                ElaborationCompleted(new Utility.EventArgs { StartTunnelReceived = StartTunnelResult != null });
            });
            task.Start();
        }

        public void GetTunnels()
        {
            var task = new Task(() =>
            {
                TunnelResult = _httpRequest.GetActiveTunnels();
                ElaborationCompleted(new Utility.EventArgs { TunnelReceived = TunnelResult != null });
            });
            task.Start();
        }

        private void ElaborationCompleted(Utility.EventArgs args)
        {
            this.elaborationCompleted?.Invoke(this, args);
        }

    }
}
