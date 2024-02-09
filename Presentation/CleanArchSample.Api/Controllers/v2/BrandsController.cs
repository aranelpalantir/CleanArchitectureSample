using Asp.Versioning;
using CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers.v2
{
    [ApiVersion("2.0")]
    public class BrandsController : BaseApiController
    {
        public BrandsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await Mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(response);
        }
      }
}
