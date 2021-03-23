using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Confluent.Kafka;

namespace CobleUp.Worker
{
    public class Worker
    {
        public void Work()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "host1:9092,host2:9092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(topics);

                while (!cancelled)
                {
                    var consumeResult = consumer.Consume(cancellationToken);

                    // handle consumed message.
                    ...
                 }

                consumer.Close();
            }
        }
    }
}
