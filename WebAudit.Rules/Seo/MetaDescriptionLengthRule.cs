using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class MetaDescriptionLengthRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var node = context.Document.DocumentNode.SelectSingleNode("//meta[@name='description']");
        var content = node?.GetAttributeValue("content", string.Empty)?.Trim();
        if (string.IsNullOrEmpty(content)) return Task.FromResult(RuleResult.Skip());
        if (content.Length < 120 || content.Length > 160) return Task.FromResult(RuleResult.Fail("Długość opisu", "Opis powinien mieć 120-160 znaków.", SeverityLevel.Info, ""));
        return Task.FromResult(RuleResult.Pass("Długość opisu", "Długość opisu jest optymalna."));
    }
}
