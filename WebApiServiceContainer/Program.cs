using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Alydnh.WebApiServiceContainer
{
    public class Program
    {


        public static void Main(string[] args)
        {
            var switchMappings = new Dictionary<string, string>()
            {
                { "/startup", typeof(Startup).AssemblyQualifiedName }
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddCommandLine(args, switchMappings);
            var configuration = configurationBuilder.Build();
            var startupName = configuration["startup"];


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:8888")
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}