using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgrokUI.Models
{
    public class Tunnel
    {
        public List<Tunnels> tunnels { get; set; }

        public string uri { get; set; }
    }

    public class Tunnels
    {
        public string name { get; set; }
        public string uri { get; set; }
        public string public_url { get; set; }
        public string proto { get; set; }
        public Config config { get; set; }
        public Metrics metrics { get; set; }
    }

    public class Config
    {
        public string addr { get; set; }
        public bool inspect { get; set; }
    }

    public class Metrics
    {
        public Conns conns { get; set; }
        public Http http { get; set; }
    }

    public class Conns
    {
        public float count { get; set; }
        public float gauge { get; set; }
        public float rate1 { get; set; }
        public float rate5 { get; set; }
        public float rate15 { get; set; }
        public float p50 { get; set; }
        public float p90 { get; set; }
        public float p95 { get; set; }
        public float p99 { get; set; }
    }

    public class Http
    {
        public float count { get; set; }
        public float rate1 { get; set; }
        public float rate5 { get; set; }
        public float rate15 { get; set; }
        public float p50 { get; set; }
        public float p90 { get; set; }
        public float p95 { get; set; }
        public float p99 { get; set; }

    }
}
