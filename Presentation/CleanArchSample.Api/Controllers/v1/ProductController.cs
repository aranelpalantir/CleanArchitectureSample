using Asp.Versioning;
using CleanArchSample.Api.Extensions;
using CleanArchSample.Application.Features.Products.Commands.CreateProduct;
using CleanArchSample.Application.Features.Products.Commands.DeleteProduct;
using CleanArchSample.Application.Features.Products.Commands.UpdateProduct;
using CleanArchSample.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchSample.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await Mediator.Send(new GetAllProductsQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IResult> CreateProduct(CreateProductCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
        {
            await Mediator.Send(request);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {
            await Mediator.Send(request);
            return Ok();
        }
    }
}
