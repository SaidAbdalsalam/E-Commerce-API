namespace ECommerce.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTimeOffset CreatedDate { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTimeOffset? LastModifiedDate { get; private set; }
    public string? LastModifiedBy { get; private set; }

    protected AuditableEntity(Guid id) : base(id) {  }
}