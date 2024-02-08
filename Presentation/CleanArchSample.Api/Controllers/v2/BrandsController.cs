using Asp.Versioning;
using CleanArchSample.Application.Features.Products.Queries.GetAllBrands;
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
            var reponse = await Mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(reponse);
        }
      }
}
