using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Protocolor;

namespace GrpcServer.Services
{
    public class ColorService : ColorGenerator.ColorGeneratorBase
    {
        private readonly ILogger<ColorService> _logger;

        public ColorService(ILogger<ColorService> logger)
        {
            _logger = logger;
        }

        public override Task<NewColor> GetRandomColor(CurrentColor request, ServerCallContext context)
        {
            return Task.FromResult(new NewColor
            {
                Color = "#0000FF"
            });
        }
    }
}