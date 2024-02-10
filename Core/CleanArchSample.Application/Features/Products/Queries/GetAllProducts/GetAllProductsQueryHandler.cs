using AutoMapper;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : CqrsHandlerBase, IRequestHandler<GetAllProductsQueryRequest, IReadOnlyList<GetAllProductsQueryResponse>>
    {
        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        public async Task<IReadOnlyList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await UnitOfWork.GetReadRepository<Product>()
                .ToListAsync(include: r => r.Include(rr => rr.Brand), cancellationToken: cancellationToken);

            var response = Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<GetAllProductsQueryResponse>>(products);

            return response;
        }
    }
}
