using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Net;


namespace CobleUp.BlazorApp.Services
{
    public class GitRequestService
    {
        public async void RequestRepo(string name)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName()
            };


            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                await producer.ProduceAsync("git-searches", new Message<Null, string> { Value = name });
            }
        }
    }
}
