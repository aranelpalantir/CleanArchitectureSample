using Asp.Versioning;
using CleanArchSample.Application.Features.Products.Queries.GetAllBrands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers.v1
{
    [ApiVersion("1.0")]
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
