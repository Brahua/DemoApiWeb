using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoWebApi.Services
{
    public class AccionesDbContextHostedService : IHostedService, IDisposable
    {
        private readonly IHostingEnvironment Enviroment;
        private Timer Timer;
        public AccionesDbContextHostedService(IHostingEnvironment Enviroment)
        {
            this.Enviroment = Enviroment;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void TareaProgramada(object state)
        {

        }

        public void Dispose()
        {
            Timer?.Dispose();
        }
    }
}
