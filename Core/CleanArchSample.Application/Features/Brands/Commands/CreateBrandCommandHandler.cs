using Bogus;
using CleanArchSample.Application.Data;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using MediatR;

namespace CleanArchSample.Application.Features.Brands.Commands
{
    internal sealed class CreateBrandCommandHandler(
        IUnitOfWork unitOfWork,
        IBrandReadRepository brandReadRepository)
        : IRequestHandler<CreateBrandCommandRequest, Unit>
    {
        public async Task<Unit> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            if (await brandReadRepository.CountAsync(cancellationToken: cancellationToken) >= 1000000)
                return Unit.Value;
            Faker faker = new("tr");
            List<Brand> brands = [];
            for (var i = 0; i <= 1000000; i++)
            {
                brands.Add(new Brand
                {
                    Name = faker.Commerce.Department(1),
                });
            }

            await unitOfWork.GetWriteRepository<Brand>().AddRangeAsync(brands, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
