namespace CleanCompanyName.DDDMicroservice.Domain.Common;

public abstract class AuditableEntity
{
    public DateTimeOffset CreatedOn { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? LastModifiedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
}