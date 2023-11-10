using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;
using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TeamManagement.Contract;
using ProductivityTools.TeamManagement.Contract.Feedback;
using ProductivityTools.TeamManagement.Contract.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProductivityTools.TeamManagement.Cmdlet.ClientCaller
{
    public class ApiClient
    {
        private readonly HttpPostClient Client;

        private static string token;

        private static string Token
        {
            get
            {
                Console.WriteLine("GetToken");
                if (string.IsNullOrEmpty(token))
                {
                    SetNewAccessToken();
                }
                else
                {
                    var tokenhanlder = new JwtSecurityTokenHandler();
                    var jwtSecurityToke = tokenhanlder.ReadJwtToken(token);
                    if (jwtSecurityToke.ValidTo < DateTime.UtcNow)
                    {
                        SetNewAccessToken();
                    }
                }
                return token;
            }
        }

        private static IConfiguration Configuration
        {
            get
            {
                IConfigurationRoot configuration = null;
                try
                {
                    configuration = new ConfigurationBuilder().AddMasterConfiguration().Build();
                    return configuration;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }

        private static void SetNewAccessToken()
        {



            Console.WriteLine("token is empty need to call identity server");
            var client = new HttpClient();

            var disco = client.GetDiscoveryDocumentAsync("https://identityserver.productivitytools.top:8010/").Result;
            Console.WriteLine($"Discovery server{disco}");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            Console.WriteLine("GetTask3Cmdlet secret");
            Console.WriteLine(Configuration["GetTask3Cmdlet"]);

            var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "TMCmdlet",
                ClientSecret = "pass1",// configuration["GetTask3Cmdlet"],
                Scope = "TM.API"
            }).Result;

            Console.WriteLine("Token response pw:");
            Console.WriteLine(tokenResponse.AccessToken);
            Console.WriteLine(tokenResponse.Error);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            token = tokenResponse.AccessToken;
        }

        public ApiClient()
        {
            var url = Configuration["url"];
            this.Client = new HttpPostClient(true);
            this.Client.SetBaseUrl(url);
           // this.Client.SetBaseUrl("https://ApiTeamManagement.productivitytools.top:8030");
            this.Client.HttpClient.SetBearerToken(Token);

            if (true)
            {
                var webApiKey = Configuration["webApiKey"];
                var password = Configuration["password"];
               
                Firebase firebase = new Firebase(webApiKey, url, password);
                var token = firebase.GetIdToken();
            }
        }

        public List<PersonFeedback> GetFeedback(List<string> initials)
        {
            var result = this.Client.PostAsync<List<PersonFeedback>>("Feedback", "GetFeedback", initials).Result;
            return result;
        }

        public void SaveFeedback(List<string> initials, string value)
        {
            var r = this.Client.PostAsync<object>("Feedback", "SaveFeedback", new SaveFeedback { Initials = initials, Value = value }).Result;
        }

        public List<PersonInternalInformation> GetInternalInformation(List<string> initials)
        {
            var result = this.Client.PostAsync<List<PersonInternalInformation>>("Internal", "GetInternalInformation", initials).Result;
            return result;
        }

        public void SaveInternalInformation(List<string> initials, string value)
        {
            var r = this.Client.PostAsync<object>("Internal", "SaveInternalInformation", new SaveInternalInformation { Initials = initials, Value = value }).Result;
        }

        public void AddPerson(string firstName, string lastName, string initials, string category)
        {
            var r = this.Client.PostAsync<object>("Person", "Add", new { FirstName = firstName, LastName = lastName, Initials = initials, Category = category });
        }

        public async Task<List<Person>> GetPeopleList()
        {
            var r = await this.Client.PostAsync<List<Person>>("Person", "GetList", new object());
            return r;
        }
    }
}
