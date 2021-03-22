using System;
using System.Collections.Generic;
using System.Linq;
    using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CobleUp.Worker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Worker>();
        }


    }
}
