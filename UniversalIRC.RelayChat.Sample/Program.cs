using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace UniversalIRC.RelayChat.Sample
{
    /// <summary>
    /// Program entry.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Commandline arguments.</param>
        private static async Task Main(string[] args)
        {
            using var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ChatHostedService>();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                })
                .UseConsoleLifetime()
                .Build();
            
            await host.RunAsync();
        }
    }
}
