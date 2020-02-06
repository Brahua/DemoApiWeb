using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoWebApi.Services
{
    public class EscribirArchivoHostedService : IHostedService, IDisposable
    {
        private readonly IHostingEnvironment Enviroment;
        private readonly string FileName = "File1.txt";
        private Timer Timer;
        public EscribirArchivoHostedService(IHostingEnvironment Enviroment)
        {
            this.Enviroment = Enviroment;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            EscribirArchivo("EscribirArchivoHostedService: Process Started");
            Timer = new Timer(TareaProgramada, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            EscribirArchivo("EscribirArchivoHostedService: Process Stopped");
            Timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void TareaProgramada(object state)
        {
            EscribirArchivo("EscribirArchivoHostedService: Escribiendo el " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        private void EscribirArchivo(string contenido)
        {
            var path = $@"{this.Enviroment.ContentRootPath}\{this.FileName}";
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(contenido);
            }
        }

        public void Dispose()
        {
            Timer?.Dispose();
        }
    }
}
