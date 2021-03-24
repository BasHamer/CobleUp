using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobleUp.Workers.Searcher.Configuration
{
    public class KafkaSettings
    {
        public static string SectionName = "Kafka";

        public string BootstrapServers { get; set; }
        public string SearchTopic { get; set; }
    }
}
