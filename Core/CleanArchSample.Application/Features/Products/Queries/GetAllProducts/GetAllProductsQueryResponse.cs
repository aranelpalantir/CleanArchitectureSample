using CleanArchSample.Application.Features.Products.Queries.GetAllProducts.Dtos;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllProducts
{
    public sealed class GetAllProductsQueryResponse
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public BrandDto? Brand { get; set; }
    }
}
