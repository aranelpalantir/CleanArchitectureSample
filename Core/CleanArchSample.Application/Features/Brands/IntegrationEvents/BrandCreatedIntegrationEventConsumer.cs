using MassTransit;
using Microsoft.Extensions.Logging;

namespace CleanArchSample.Application.Features.Brands.IntegrationEvents
{
    public sealed class BrandCreatedIntegrationEventConsumer(ILogger<BrandCreatedIntegrationEventConsumer> logger) : IConsumer<BrandCreatedIntegrationEvent>
    {
        public Task Consume(ConsumeContext<BrandCreatedIntegrationEvent> context)
        {
            logger.LogInformation("Brand created: {@Brand}", context.Message);

            return Task.CompletedTask;
        }
    }
}
