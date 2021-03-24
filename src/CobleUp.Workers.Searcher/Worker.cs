using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Confluent.Kafka;
using CobleUp.Workers.Searcher.Configuration;
using Microsoft.Extensions.Options;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace CobleUp.Workers.Searcher
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

        public void Download()
        {
            RemoteWebDriver driver;


            //CHROME and IE            
            ChromeOptions Options = new ChromeOptions();

            driver = new RemoteWebDriver(
              new Uri("http:10.0.0.127:4444/wd/hub/"), Options.ToCapabilities(), TimeSpan.FromSeconds(30));
            try
            {

                driver.Navigate().GoToUrl("https://www.google.com/ncr");
                IWebElement query = driver.FindElement(By.Name("q"));
                query.SendKeys("webdriver");
                query.Submit();
            }
            finally
            {
                Console.WriteLine("Video: " + "" + driver.SessionId);
                driver.Quit();
            }
        }
    }
}
