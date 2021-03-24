using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;

namespace CobleUp.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var CancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                Console.WriteLine("Canceling...");
                CancellationTokenSource.Cancel();
                e.Cancel = true;
            };

            var webHost = CreateHostBuilder(args).Build();
            var worker = (Worker)webHost.Services.GetService(typeof(Worker));
            worker.Work(CancellationTokenSource.Token);

            webHost.Run();
        }
        private static Startup Startup { get; set; }
        private static CancellationTokenSource CancellationTokenSource { get; set; }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                {
                    configuration.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    configuration
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                    IConfigurationRoot configurationRoot = configuration.Build();
                    Startup = new Startup(configurationRoot, CancellationTokenSource);
                })
                .ConfigureServices(x=>Startup.ConfigureServices(x));
    }
}
