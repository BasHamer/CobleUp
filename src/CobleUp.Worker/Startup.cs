using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CobleUp.Worker.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CobleUp.Worker
{
    public class Startup
    {
        public Startup(IConfiguration configuration, CancellationTokenSource cancellationTokenSource)
        {
            Configuration = configuration;
            CancellationTokenSource = cancellationTokenSource;
        }

        public IConfiguration Configuration { get; }
        public CancellationTokenSource CancellationTokenSource { get; }

       public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfig(Configuration);
            services.AddSingleton<Worker>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
