using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ESchool.Frontend.Infrastructure;
using ESchool.Frontend.Network.Authentication;
using Flurl.Http;
using Flurl.Http.Configuration;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESchool.Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseUrl"))
            });

            builder.Services.AddScoped<IFlurlClient>(sp =>
            {
                var authService = sp.GetRequiredService<ICodeFlowAuthService>();
                var client = sp.GetRequiredService<HttpClient>();
                return new FlurlClient(client)
                {
                    Settings = new ClientFlurlHttpSettings
                    {
                        JsonSerializer = new SystemTextJsonSerializer(new JsonSerializerOptions()),
                        BeforeCallAsync = call =>
                        {
                            var tcs = new TaskCompletionSource();
                            authService.AccessToken.Take(1).Subscribe(token =>
                            {
                                call.Request.Headers.Add("Authorization", $"Bearer {token}");
                                tcs.SetResult();
                            });
                            return tcs.Task;
                        }
                    }
                };
            });

            builder.Services.AddTransient(_ => new OidcClient(new OidcClientOptions
            {
                Authority = builder.Configuration.GetValue<string>("Auth:Authority"),
                Scope = builder.Configuration.GetValue<string>("Auth:Scopes"),
                ClientId = builder.Configuration.GetValue<string>("Auth:ClientId"),
                LoadProfile = true,
                RefreshDiscoveryDocumentForLogin = true,
                RedirectUri = builder.Configuration.GetValue<string>("Auth:RedirectUri"),
                PostLogoutRedirectUri = builder.Configuration.GetValue<string>("Auth:PostLogoutRedirectUri")
            }));

            builder.Services.AddTransient(sp => new FlurlClient(sp.GetRequiredService<HttpClient>()));

            await builder.Build().RunAsync();
        }
    }
}