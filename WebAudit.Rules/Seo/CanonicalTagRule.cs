using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class CanonicalTagRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var node = context.Document.DocumentNode.SelectSingleNode("//link[@rel='canonical']");
        if (node == null) return Task.FromResult(RuleResult.Fail("Brak tagu canonical", "Brak tagu link canonical.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Tag canonical", "Znaleziono tag canonical."));
    }
}
