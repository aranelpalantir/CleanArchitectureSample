using Asp.Versioning;
using CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers.v2
{
    [ApiVersion("2.0")]
    public class BrandController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await Mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(response);
        }
      }
}
