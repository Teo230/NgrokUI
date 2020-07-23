using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgrokUI.Models
{
    public class EnumDictionary
    {
        public enum AuthenticationType
        {
            Sys = 0,
            Basic,
            Bearer,
            None
        }

        public enum Protocols
        {
            Sys = 0,
            http,
            tcp,
            tls
        }
    }
}
