using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UrgentCareCommon.Helpers
{
    public sealed class HttpClientSingleton
    {
        private static readonly HttpClient instance = new HttpClient();

        private HttpClientSingleton() { }

        public static HttpClient Instance
        {
            get
            {
                return instance;
            }
        }
    }

    public static class HttpHelper
    {
        public static async Task<string> SendGetRequestAsync(string requestUri, string authHeaderName = "", string authHeaderValue = "")
        {
            using (var client = new HttpClient())
            {
                if(!string.IsNullOrEmpty(authHeaderName))
                {
                    client.DefaultRequestHeaders.Add(authHeaderName, authHeaderValue);
                }
                var result = await client.GetAsync(requestUri);

                string resultContent = await result.Content.ReadAsStringAsync();
                return resultContent;
            }
        }

        public static async Task<HttpResponseMessage> SendPostRequestAsync(string requestUri, string payload, string authHeaderName = "", string authHeaderValue = "")
        {
            using (var client = new HttpClient())
            {
                if(!string.IsNullOrEmpty(authHeaderName))
                {
                    client.DefaultRequestHeaders.Add(authHeaderName, authHeaderValue);
                }
                return await client.PostAsync(requestUri, new StringContent(payload, Encoding.UTF8, "application/json"));
            }
        }
    }
}
