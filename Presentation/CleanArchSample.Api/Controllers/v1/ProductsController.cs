using Asp.Versioning;
using CleanArchSample.Application.Features.Products.Commands.CreateProduct;
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
            var response = await Mediator.Send(new GetAllProductsQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {
            await Mediator.Send(request);
            return Ok();
        }
    }
}
