using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using platzi_asp_net_core.Models;

namespace platzi_asp_net_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            // La linea anterior esta comentada para que no inicie hasta asegurarme
            // que no esten todos los datos cargados
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()){
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<EscuelaContext>();
                    context.Database.EnsureCreated();
                }
                catch (System.Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocurrio un error");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
