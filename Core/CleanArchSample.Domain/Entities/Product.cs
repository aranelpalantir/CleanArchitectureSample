using CleanArchSample.Domain.Primitives;
using MediatR;

namespace CleanArchSample.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductCategory> ProductCategories = new List<ProductCategory>();
    }
}
