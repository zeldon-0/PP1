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
using PP2.ServiceLayer.DataLayer.Contracts;
using CancellationToken = System.Threading.CancellationToken;

namespace PP1.ServiceLayer.BackgroundServices
{
    public class ConsumerService : BackgroundService
    {
       // private readonly IDataProducer _dataProducer;
        private readonly IMessageRepo _messageRepo;

        public ConsumerService(//IDataProducer dataProducer, 
            IMessageRepo messageRepo
        )
        {
           // _dataProducer = dataProducer;
            _messageRepo = messageRepo;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => Consume(stoppingToken));
            return Task.CompletedTask;
        }

        private async Task Consume(CancellationToken stoppingToken)
        {
            var message = new MessageModel() {Id = 1, Data = 1};//_dataProducer.Produce();
            _messageRepo.Save(message);
        }
    }
}
