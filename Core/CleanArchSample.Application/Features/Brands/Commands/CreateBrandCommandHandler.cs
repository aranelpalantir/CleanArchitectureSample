using CleanArchSample.Application.Abstractions.Data;
using CleanArchSample.Application.Abstractions.MessageBroker;
using CleanArchSample.Application.Features.Brands.IntegrationEvents;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using MediatR;

namespace CleanArchSample.Application.Features.Brands.Commands
{
    internal sealed class CreateBrandCommandHandler(
        IUnitOfWork unitOfWork,
        IBrandRepository brandRepository,
        IIntegrationEventBus integrationEventBus)
        : IRequestHandler<CreateBrandCommandRequest, Unit>
    {
        public async Task<Unit> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var brand = new Brand();
            brand.Name = request.Name;
            await brandRepository.AddAsync(brand, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await integrationEventBus.PublishAsync(new BrandCreatedIntegrationEvent { Id = brand.Id, Name = brand.Name }, cancellationToken);
            return Unit.Value;
        }
    }
}
