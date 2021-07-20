using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Protocolor;

namespace GrpcServer
{
    public class Program
    {
        //    public static void Main(string[] args)
        //    {
        //        CreateHostBuilder(args).Build().Run();
        //    }

        //    // Additional configuration is required to successfully run gRPC on macOS.
        //    // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        //    public static IHostBuilder CreateHostBuilder(string[] args) =>
        //        Host.CreateDefaultBuilder(args)
        //            .ConfigureWebHostDefaults(webBuilder =>
        //            {
        //                webBuilder.UseStartup<Startup>();
        //            });
        //}

        static void Main(string[] args)
        {
            const int Port = 50051;

            Server server = new Server
            {
                Services = { ColorGenerator.BindService(new SayColorImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();
            Console.WriteLine("Server Start On Localhost:50051");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    }

    class SayColorImpl : ColorGenerator.ColorGeneratorBase
    {
        public override Task<NewColor> GetRandomColor(CurrentColor request, ServerCallContext context)
        {
            return Task.FromResult(new NewColor
            {
                Color = "#0000FF"
            });
        }
    }
}
