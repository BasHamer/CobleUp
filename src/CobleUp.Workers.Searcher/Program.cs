using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CobleUp.Workers.Searcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();
            var CancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                Console.WriteLine("Canceling...");
                CancellationTokenSource.Cancel();
                e.Cancel = true;
            };

            
            var worker = (Worker)webHost.Services.GetService(typeof(Worker));
            worker.Work(CancellationTokenSource.Token);

            webHost.Run();
        }
        private static Startup Startup { get; set; }
        private static CancellationTokenSource CancellationTokenSource { get; set; }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    config
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                    IConfigurationRoot configurationRoot = config.Build();
                    Startup = new Startup(configurationRoot);
                })
                .ConfigureServices(x => Startup.ConfigureServices(x));
    }
}
