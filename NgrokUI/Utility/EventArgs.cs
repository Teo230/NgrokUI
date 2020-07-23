using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgrokUI.Utility
{
    public class EventArgs
    { 
        public bool? StartTunnelReceived { get; set; } = null;
        public bool? TunnelReceived { get; set; } = null;
    }
}
