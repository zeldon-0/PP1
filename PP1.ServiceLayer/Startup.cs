using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PP1.ServiceLayer.BackgroundServices;
using PP1.ServiceLayer.Infrastructure;
using PP1.ServiceLayer.Services;

namespace PP1.ServiceLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHostedService<ConsumerService>();
            services.Configure<ConsumerConfig>(Configuration.GetSection("ConsumerConfig"));
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<DataProducer>().As<IDataProducer>().SingleInstance();
            //builder.RegisterType<DataProducerWithLock>().As<IDataProducer>().SingleInstance();
            //builder.RegisterType<BlockingDataProducer>().As<IDataProducer>().SingleInstance();
            //builder.RegisterType<DataProducerWithMutex>().As<IDataProducer>().SingleInstance();
            //builder.RegisterType<DataProducerWithSemaphore>().As<IDataProducer>().SingleInstance();
            //builder.RegisterType<DataProducerWithMonitor>().As<IDataProducer>().SingleInstance();
            //builder.RegisterType<DataProducerWithSpinLock>().As<IDataProducer>().SingleInstance();
            //builder.RegisterType<ConcurrentDataProducer>().As<IDataProducer>().SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
