using AutoMapper;
using CleanArchSample.Application.Interfaces.UnitOfWorks;

namespace CleanArchSample.Application.Features.Common
{
    public abstract class CqrsHandlerBase
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        protected CqrsHandlerBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}
