using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ESchool.IdentityProvider.Interface.Features.Users;
using Flurl.Http;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Microsoft.AspNetCore.Http;

namespace ESchool.Tools.TenantLogin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var options = new OidcClientOptions
            {
                Authority = "https://localhost:5301",
                ClientId = "test",
                RedirectUri = "http://localhost:4000/oauth",
                Scope = "openid profile user_role classregisterapi.readwrite identityproviderapi.readwrite homeassignmentsapi.readwrite testingapi.readwrite",
                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                Browser = new NativeBrowser(),
                RefreshDiscoveryDocumentForLogin = true,
                LoadProfile = false
            };
            options.Policy.Discovery.ValidateIssuerName = false;
            var oidcClient = new OidcClient(options);

            var loginResult = await oidcClient.LoginAsync();
            if (loginResult.IsError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sikertelen bejelentkezés.");
                return;
            }

            Console.WriteLine("Sikeres bejelentkezés!");
            Console.WriteLine("1 - Access token kiírása");
            Console.WriteLine("2 - Bérlő kiválasztása");
            while (true)
            {
                Console.Write("Választott lehetőség: ");
                var option = Console.ReadLine();
                if (option == "1")
                {
                    Console.WriteLine("Access token:");
                    Console.WriteLine(loginResult.AccessToken);
                    Console.ReadLine();
                    return;
                }

                if (option == "2")
                {
                    using var client = new HttpClient();
                    var response = await "https://localhost:5301/api/users/me"
                        .WithOAuthBearerToken(loginResult.AccessToken)
                        .GetJsonAsync<UserDetailsResponse>();

                    for (int i = 0; i < response.Tenants.Count; i++)
                    {
                        Console.WriteLine($"{i} - {response.Tenants[i].Name}");
                    }

                    Console.Write("Választott bérlő: ");
                    var tenantIndex = int.Parse(Console.ReadLine());
                    var tenantId = response.Tenants[tenantIndex].Id;

                    var authorizeState = await oidcClient.PrepareLoginAsync();
                    var browser = new NativeBrowser();
                    var browserResult =
                        await browser.InvokeAsync(new BrowserOptions(authorizeState.StartUrl,
                            authorizeState.RedirectUri));
                    var code = HttpUtility.ParseQueryString(browserResult.Response).Get("code");

                    var result = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
                    {
                        Address = "https://localhost:5301/connect/token",
                        Code = code,
                        GrantType = "authorization_code",
                        ClientId = "test",
                        RedirectUri = "http://localhost:4000/oauth",
                        CodeVerifier = authorizeState.CodeVerifier,
                        Parameters = new Dictionary<string, string>
                        {
                            { "tenant_id", tenantId.ToString() }
                        }
                    });

                    if (result.IsError)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Hiba történt a bejelentkezéskor.");
                        return;
                    }

                    Console.WriteLine("Sikeres belejentkezés!");
                    Console.WriteLine("Access token:");
                    Console.WriteLine(result.AccessToken);
                    Console.ReadLine();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ismeretlen lehetőség.");
                Console.ResetColor();
            }

            
        }
    }
}