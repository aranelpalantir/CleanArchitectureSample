using MediatR;

namespace CleanArchSample.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<Unit>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public required IList<int> CategoryIds { get; set; }
    }
}
