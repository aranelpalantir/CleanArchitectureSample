namespace CleanArchSample.Domain.Primitives;

public class EntityBase() : IEntityBase
{
    public int Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
}