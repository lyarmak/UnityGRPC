using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        static void Main(string[] args)
        {
            const int port = 50051;

            var server = new Server
            {
                Services = { ColorGenerator.BindService(new SayColorImpl()) },
                Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
            };
            server.Start();
            Console.WriteLine("Server Start On Localhost:50051");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    }

    internal class SayColorImpl : ColorGenerator.ColorGeneratorBase
    {
        private readonly Random _random = new Random();

        public override Task<NewColor> GetRandomColor(CurrentColor request, ServerCallContext context)
        {
            var randomColor = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
            Console.WriteLine($"Sent Color: {randomColor} ");

            return Task.FromResult(new NewColor
            {
                Color = randomColor.Name
            });
        }
    }
}
