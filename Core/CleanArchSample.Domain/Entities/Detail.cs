using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Domain.Entities
{
    public class Detail : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
