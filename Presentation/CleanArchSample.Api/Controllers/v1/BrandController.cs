using Asp.Versioning;
using CleanArchSample.Application.Features.Brands.Commands;
using CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class BrandController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await Mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBrand(CreateBrandCommandRequest request)
        {
            await Mediator.Send(request);
            return Ok();
        }

    }
}
