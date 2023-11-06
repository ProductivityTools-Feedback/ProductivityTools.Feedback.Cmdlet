using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.TeamManagement.Cmdlet.ClientCaller
{
    internal class Firebase
    {
        public string WebApiKey { get; set; }
        public string Url { get; set; }

        public Firebase(string webApiKey, string url)
        {
            this.WebApiKey = webApiKey;
            this.Url = url;
        }

        async Task<string> GetIdToken(string custom_token)
        {
            var HttpClient = new HttpClient();
            Uri url = new Uri($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithCustomToken?key={this.WebApiKey}");

            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            object obj = new { token = custom_token, returnSecureToken = true };
            var dataAsString = JsonConvert.SerializeObject(obj);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await HttpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var resultAsString = await response.Content.ReadAsStringAsync();
                    TokenResponse result = JsonConvert.DeserializeObject<TokenResponse>(resultAsString);
                    return result.idToken;
                }
                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<string> GetCustomToken()
        {
            var HttpClient = new HttpClient();
            Uri url = new Uri($"{this.Url}CustomToken/Get");

            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var resultAsString = await response.Content.ReadAsStringAsync();
                // T result = JsonConvert.DeserializeObject<T>(resultAsString);
                return resultAsString;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}
