using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NgrokUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NgrokUI.API
{
    public class HttpRequest
    {
        #region Prop
        private string Server { get; set; }
        private string BasicUrl { get; set; }

        #endregion

        #region Ctr
        public HttpRequest()
        {
            Server = "http://127.0.0.1:4040";
            BasicUrl = "/api";
        }
        #endregion

        #region Methods

        public Tunnel GetActiveTunnels()
        {
            var request = "/tunnels";

            var url = Server + BasicUrl + request;

            return Get<Tunnel>(Server, url);
        }

        public bool StartTunnel(int port, EnumDictionary.Protocols protocol)
        {
            var request = "/tunnels";
            var url = Server + BasicUrl + request;

            dynamic body = new JObject();

            body.addr = port.ToString();
            switch (protocol)
            {
                case EnumDictionary.Protocols.http:
                    body.proto = "http";
                    break;
                case EnumDictionary.Protocols.tcp:
                    body.proto = "tcp";
                    break;
                case EnumDictionary.Protocols.tls:
                    body.proto = "tls";
                    break;
            }
            body.name = "local";

            Post<Tunnel>(Server, url, body);

            return true;
        }

        #region HttpRequestMethods
        public T Post<T>(string server, string url, dynamic content)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(server);

                var result = httpClient.PostAsync(url, new StringContent(content.ToString(), Encoding.UTF8, "application/json")).Result;

                if (result.IsSuccessStatusCode)
                {
                    var resultString = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(resultString);
                }
                else
                {
                    if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new Exception();
                    throw new Exception("Call failure");
                }
            }
        }

        public T Get<T>(string server, string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(server);

                var result = httpClient.GetAsync(url).Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultString = result.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(resultString);
                }
                else
                {
                    if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new Exception();
                    throw new Exception("Call failure");
                }
            }
        }

        #endregion

        #endregion
    }
}
