using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Domain.Entities
{
    public class Detail : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
