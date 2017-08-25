using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ASADemoTelemetryDriver.Interfaces;

namespace ASADemoTelemetryDriver
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        private static EventHubClient eventHubClient;
        private static IEnumerable<TempReading> temperatureReadings;
       
        static void Main(string[] args)
        {           
            SetupServices();

            TempJsonProcessor tempProcessor = new TempJsonProcessor(_serviceProvider.GetService<ITemperatureDataReader>());
            temperatureReadings = tempProcessor.LoadTempReadings();

            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            // Creates an EventHubsConnectionStringBuilder object from the connection string, and sets the EntityPath.
            // Typically, the connection string should have the entity path in it, but for the sake of this simple scenario
            // we are using the connection string from the namespace.
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(ConfigurationManager.AppSettings["EhConnectionString"])
            {
                EntityPath = ConfigurationManager.AppSettings["EhEntityPath"]
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            await SendMessagesToEventHub();

            await eventHubClient.CloseAsync();

            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }

        private static async Task SendMessagesToEventHub()
        {
            int count = 0;

            foreach (TempReading currentReading in temperatureReadings)
            {
                try
                {
                    var message = JsonConvert.SerializeObject(currentReading);
                    //Console.WriteLine($"Sending message: {message}");
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                    count += 1;

                    if (count == 100)
                    {
                        count = 0;
                        Console.Write(".");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                }

                //await Task.Delay(10);
            }
            Console.WriteLine(".");
            Console.WriteLine($"All messages sent.");
        }

        private static void SetupServices()
        {
            _serviceProvider = new ServiceCollection()
                .AddTransient<ITemperatureDataReader, TemperatureDataReader>()
                .BuildServiceProvider();
        }
    }
}
