using Newtonsoft.Json;
using OpenAIDemo.Core;
using OpenAIDemo.Models;
using System.Net.Http.Headers;
using System.Text;

namespace OpenAIDemo.Helper
{
    public static class AppHelper
    {
        public static async Task<string> WebPostAsync(this string URI, object obj, int Timeout = 60)
        {
            using (var client = new HttpClient())
            {
                // clear the default headers to avoid issues
                client.DefaultRequestHeaders.Clear();

                // add header authorization and use your API KEY
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalContext.SystemConfig.OPENAI_API_KEY);
                client.Timeout = TimeSpan.FromSeconds(Timeout);
                var serializedProduct = JsonConvert.SerializeObject(obj);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(URI, content);
                if (response.IsSuccessStatusCode)
                {
                    var productJsonString = await response.Content.ReadAsStringAsync();
                    return productJsonString.ToString();
                }

            }

            return string.Empty;
        }

        public static void ToSuccess<T>(this TData<T> data)
        {
            data.Tag = 1;
        }

        public static void ToSuccess<T>(this TData<T> data, T result)
        {
            data.Tag = 1;
            data.Data = result;
        }

        public static void ToError(this TData data, string Message)
        {
            data.Tag = 0;
            data.Message = Message;
        }
    }
}
