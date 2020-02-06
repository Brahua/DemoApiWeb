using DemoWebApi.Contexts;
using DemoWebApi.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoWebApi.Services
{
    public class ConsumeScopedService : IHostedService, IDisposable
    {
        private Timer _timer;
        public IServiceProvider Services { get; }
        public ConsumeScopedService(IServiceProvider services)
        {
            Services = services;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(TareaProgramada, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void TareaProgramada(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var mensaje = "ConsumeScopedService: Escribiendo el " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                HostedServiceLog log = new HostedServiceLog() { Mensaje = mensaje };
                context.HostedServiceLogs.Add(log);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
