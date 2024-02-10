using AutoMapper;
using Bogus;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchSample.Application.Features.Brands.Commands
{
    public class CreateBrandCommandHandler : CqrsHandlerBase, IRequestHandler<CreateBrandCommandRequest, Unit>
    {
        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            if (await UnitOfWork.GetReadRepository<Brand>().CountAsync(cancellationToken: cancellationToken) >= 1000000)
                return Unit.Value;
            Faker faker = new("tr");
            List<Brand> brands = new();
            for (var i = 0; i <= 1000000; i++)
            {
                brands.Add(new Brand
                {
                    Name = faker.Commerce.Department(1),
                    CreatedBy = "-",
                    CreatedDate = DateTimeOffset.UtcNow
                });
            }

            await UnitOfWork.GetWriteRepository<Brand>().AddRangeAsync(brands, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
