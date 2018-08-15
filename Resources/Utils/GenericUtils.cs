using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class GenericUtils
    {
        public static async Task<dynamic> PutAsync(string url, dynamic param, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Add("pst", "Tests");
            HttpResponseMessage response = await client.PutAsync(url, PrepareContent(param));
            return response;
        }

        public static async Task<dynamic> GetAsync(string url, string token)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Add("pst", "Tests");
                var response = await client.GetAsync(url);
                return JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<dynamic> PostAsync(string url, dynamic param, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Add("pst", "Tests");
            HttpResponseMessage response = await client.PostAsync(url, PrepareContent(param));
            return response;
        }

        public static async Task<dynamic> DeleteAsync(string url, string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            client.DefaultRequestHeaders.Add("pst", "Tests");
            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private static ByteArrayContent PrepareContent(dynamic param)
        {
            var content = JsonConvert.SerializeObject(param);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent =
                new ByteArrayContent(buffer) { Headers = { ContentType = new MediaTypeHeaderValue("application/json") } };
            return byteContent;
        }

        private void AvoidSslCertificate() => ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => { return true; };
    }
}
