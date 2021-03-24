using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobleUp.Worker.Configuration
{
    public static class Extensions
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            services.Configure<KafkaSettings>(
                config.GetSection(KafkaSettings.SectionName));

            return services;
        }
    }
}
