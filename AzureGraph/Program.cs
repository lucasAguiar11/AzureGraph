using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace AzureGraph
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create("x")
                .WithTenantId("x")
                .WithClientSecret("x")
                .Build();
            var authenticationProvider = new ClientCredentialProvider(confidentialClientApplication);
            var graphServiceClient = new GraphServiceClient(authenticationProvider);

            await ReadApp(graphServiceClient);
            // await DeleteApp(graphServiceClient);
        }

        private static async Task ReadApp(GraphServiceClient client)
        {
            var apps = await client.Applications
                .Request().GetAsync();

            Console.WriteLine(JsonConvert.SerializeObject(apps));
        }

        private static async Task DeleteApp(GraphServiceClient client)
        {
            await client.Applications["d4366455-8c1e-41c9-ad53-4530ffe5d438"]
                .Request().DeleteAsync();
        }

        private static async Task CreateApp(GraphServiceClient client)
        {
            var application = new Application
            {
                DisplayName = "EC Teste - PIX",
            };

            var app = await client.Applications
                .Request()
                .AddAsync(application);

            Console.WriteLine(JsonConvert.SerializeObject(app));
        }

        private static async Task AddSecret(GraphServiceClient client)
        {
            var passwordCredential = new PasswordCredential
            {
                DisplayName = "Teste EC - Senha",
            };

            var app = await client.Applications["abf9cb4b-3b48-4530-a487-9ff6dfbc7e44"]
                .AddPassword(passwordCredential)
                .Request()
                .PostAsync();

            Console.WriteLine(JsonConvert.SerializeObject(app));
        }

        private static async Task ReadUsers(GraphServiceClient client)
        {
            var users = await client.Users.Request().GetAsync();
            Console.WriteLine(JsonConvert.SerializeObject(users));
        }

        private static async Task CreateUser(GraphServiceClient client)
        {
            var id = Guid.NewGuid().ToString();
            var user = new User
            {
                AccountEnabled = true,
                MailNickname = "Sla",
                DisplayName = "Adele Vance",
                Mail = "adele@v.com.br",
                UserPrincipalName = "adele_v.com.br#EXT#@xxxxx.onmicrosoft.com",
                ODataType = "microsoft.graph.user",
                Id = id,
                
                AdditionalData = new Dictionary<string, object>()
                    { { "@odata.id", $"https://graph.microsoft.com/v2/cb928803-7a0b-4aed-bfae-fcc266566780/directoryObjects/{id}/Microsoft.DirectoryServices.User" } },
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = false,
                    Password = "xWwvJ]6NMw+bWH-d"
                },
            };


            var a = await client.Users
                .Request()
                .AddAsync(user);

            Console.WriteLine(a);
        }
    }
}