using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Confluent.Kafka;
using CobleUp.Worker.Configuration;
using Microsoft.Extensions.Options;

namespace CobleUp.Worker
{
    public class Worker
    {
        public Worker(IOptions<KafkaSettings> kafkaSettings)
        {
            KafkaSettings = kafkaSettings.Value;
        }

        private KafkaSettings KafkaSettings { get; }

        public void Work(System.Threading.CancellationToken token)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers,
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(KafkaSettings.SearchTopic);

                while (!token.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(token);

                    Console.WriteLine(consumeResult.Message);
                 }

                consumer.Close();
            }
        }
    }
}
