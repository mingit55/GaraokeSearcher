using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GaraokeSearcher.Libs
{
    public class HttpApiClient
    {
        static private Boolean isInit = false;
        static private HttpClient client = new HttpClient();

        static public HttpClient getClient()
        {
            if (isInit == false)
            {
                client.BaseAddress = new Uri("http://localhost:8080/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                isInit = true;
            }
            return client;
        }

        static public async Task<JObject> Call(string path)
        {
            var response = await getClient().GetAsync(path);
            string jsonResult = await response.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(jsonResult);
            return jObject;
        }
    }
}
