using System;
using System.Net.Http;
using IdentityModel.Client;

namespace ThirdParty
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync("http://localhost:5000").Result;
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }
            var tokenClient = new TokenClient(disco.TokenEndpoint, new TokenClientOptions()
            {
                ClientId = "client",
                ClientSecret = "secret"
            });
            var tokenResponse = tokenClient.RequestClientCredentialsTokenAsync("api").Result;

            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var response = httpClient.GetAsync("http://localhost:5001/api/values").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
