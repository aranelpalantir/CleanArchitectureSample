using AutoMapper;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllBrands
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQueryRequest, IReadOnlyList<GetAllBrandsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBrandsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await _unitOfWork.GetReadRepository<Brand>()
                .ToListAsync(cancellationToken: cancellationToken);

            var response = _mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<GetAllBrandsQueryResponse>>(brands);

            return response;
        }
    }
}
