using CleanArchSample.Application.Features.Products.Queries.GetAllBrands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var reponse = await _mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(reponse);
        }
    }
}
