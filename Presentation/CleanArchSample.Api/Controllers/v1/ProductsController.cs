using Asp.Versioning;
using CleanArchSample.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductsController : BaseApiController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var reponse = await Mediator.Send(new GetAllProductsQueryRequest());
            return Ok(reponse);
        }
    }
}
