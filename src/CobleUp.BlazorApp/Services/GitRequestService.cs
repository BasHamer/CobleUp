using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Net;
using CobleUp.BlazorApp.Configuration;
using Microsoft.Extensions.Options;

namespace CobleUp.BlazorApp.Services
{
    public class GitRequestService
    {
        public GitRequestService(IOptions<KafkaSettings> kafkaSettings)
        {
            KafkaSettings = kafkaSettings.Value;
        }

        private KafkaSettings KafkaSettings { get; }
        public async void RequestRepo(string name)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers,
                ClientId = Dns.GetHostName()
            };


            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                await producer.ProduceAsync(KafkaSettings.SearchTopic, new Message<Null, string> { Value = name });
            }
        }
    }
}
