using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class MetaDescriptionExistsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var node = context.Document.DocumentNode.SelectSingleNode("//meta[@name='description']");
        var content = node?.GetAttributeValue("content", string.Empty)?.Trim();
        if (string.IsNullOrEmpty(content)) return Task.FromResult(RuleResult.Fail("Brak opisu", "Brak tagu meta description.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Meta description", "Znaleziono meta description."));
    }
}
