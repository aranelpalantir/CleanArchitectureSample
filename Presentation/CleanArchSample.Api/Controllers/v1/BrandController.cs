using Asp.Versioning;
using CleanArchSample.Application.Features.Brands.Commands;
using CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BrandController : BaseApiController
    {
        public BrandController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await Mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandCommandRequest request)
        {
            await Mediator.Send(request);
            return Ok();
        }

    }
}
