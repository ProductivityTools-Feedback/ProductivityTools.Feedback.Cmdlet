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
        public string Password { get; set; }
        private string User { get { return "cmdlet"; } }

        public Firebase(string webApiKey, string url, string password)
        {
            this.WebApiKey = webApiKey;
            this.Url = url;
            this.Password = password;
        }


        public async Task<string> GetIdToken()
        {
            var customToken = await this.GetCustomToken();
            var idToken = await this.GetIdToken(customToken);
            return idToken;
        }

        private async Task<string> GetIdToken(string custom_token)
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
            Uri url = new Uri($"{this.Url}/CustomToken/Get?user={this.User}&password={this.Password}");

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
