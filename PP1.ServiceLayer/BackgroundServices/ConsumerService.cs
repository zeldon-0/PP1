using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Options;
using PP1.ServiceLayer.Infrastructure;
using PP1.ServiceLayer.Services;
using CancellationToken = System.Threading.CancellationToken;

namespace PP1.ServiceLayer.BackgroundServices
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILifetimeScope _lifetimeScope;
        private ConsumerConfig _consumerConfig;

        public ConsumerService(
            ILifetimeScope lifetimeScope,
            IOptions<ConsumerConfig> consumerConfig)
        {
            _lifetimeScope = lifetimeScope;
            _consumerConfig = consumerConfig.Value;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            Parallel.For(0, _consumerConfig.ConsumerCount,
                async (index) => await Consume(index, stoppingToken));

            return Task.CompletedTask;
        }

        private async Task Consume(int consumerIndex, CancellationToken stoppingToken)
        {
            using var lifeTimeScope = _lifetimeScope.BeginLifetimeScope();
            var dataProducer = lifeTimeScope.Resolve<IDataProducer>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumedValue = dataProducer.Produce();

                    if (consumedValue == null)
                    {
                        this.Dispose();
                    }
                    else
                    {
                        Console.WriteLine($"Consumer #{consumerIndex} consumed the value: {consumedValue.Data}.");
                        Task.Delay(100).Wait();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine($"Consumer #{consumerIndex} stopped its execution due to a cancellation request.");

        }
    }
}
