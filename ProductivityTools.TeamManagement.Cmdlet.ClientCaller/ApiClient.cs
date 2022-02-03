using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;
using ProductivityTools.SimpleHttpPostClient;
using ProductivityTools.TeamManagement.Contract.Feedback;
using ProductivityTools.TeamManagement.Contract.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

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

        private static void SetNewAccessToken()
        {
            IConfigurationRoot configuration = null;
            try
            {
                configuration = new ConfigurationBuilder().AddMasterConfiguration().Build();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }



            Console.WriteLine("token is empty need to call identity server");
            var client = new HttpClient();

            var disco = client.GetDiscoveryDocumentAsync("https://identityserver.productivitytools.tech:8010/").Result;
            Console.WriteLine($"Discovery server{disco}");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            Console.WriteLine("GetTask3Cmdlet secret");
            Console.WriteLine(configuration["GetTask3Cmdlet"]);

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
            this.Client = new HttpPostClient(true);
            this.Client.SetBaseUrl("https://localhost:44386");
            this.Client.SetBaseUrl("https://ApiTeamManagement.productivitytools.tech:8030");
            this.Client.HttpClient.SetBearerToken(Token);
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
    }
}
