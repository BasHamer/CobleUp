using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace CobleUp.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            CreateHostBuilder(args).Build().Run();
        }
        private static Startup Startup { get; set; }

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
                    Startup = new Startup(configurationRoot);
                })
                .ConfigureServices(x=>Startup.ConfigureServices(x));
    }
}
