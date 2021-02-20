using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.OidcClient.Browser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ESchool.Tools.TenantLogin
{
    public class NativeBrowser : IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var taskCompletionSource = new TaskCompletionSource<BrowserResult>();
            using var cancellationTokenSource = new CancellationTokenSource();
            var runTask = Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder.Configure(app =>
                    {
                        app.Run(context => 
                        { 
                            taskCompletionSource.SetResult(new BrowserResult
                            {
                                Response = context.Request.QueryString.Value,
                                ResultType = (context.Request.QueryString.Value?.Contains("error") ?? false) ? BrowserResultType.HttpError
                                    : BrowserResultType.Success,
                                Error = "Anyád"
                            });
                            return Task.CompletedTask;
                        });
                    });
                    
                    builder.UseUrls("http://localhost:4000");
                    builder.UseKestrel();
                })
                .Build()
                .RunAsync(cancellationTokenSource.Token);

            var process = Process.Start(new ProcessStartInfo(options.StartUrl)
            {
                UseShellExecute = true
            });

            var finished = await Task.WhenAny(Task.Delay(60000), taskCompletionSource.Task);
            
            cancellationTokenSource.Cancel();
            if (finished == taskCompletionSource.Task)
            {
                return await taskCompletionSource.Task;
            }

            return new BrowserResult
            {
                ResultType = BrowserResultType.Timeout,
                Error = "Időtúllépés történt. Próbáld újra."
            };
        }
    }
}