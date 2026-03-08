using WebAudit.Domain.Enums;
using WebAudit.Domain.ValueObjects;

namespace WebAudit.Domain.Enitites;

public class AuditIssue
{
    public Guid Id { get; private set; }
    public Category Category { get; private set; }
    public RuleResult Result { get; private set; }

    public AuditIssue(Category category, RuleResult result)
    {
        Id = Guid.NewGuid();
        Category = category;
        Result = result;
    }
}