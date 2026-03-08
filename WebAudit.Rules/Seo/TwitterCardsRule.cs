using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class TwitterCardsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.SocialMedia;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var tc = context.Document.DocumentNode.SelectSingleNode("//meta[@name='twitter:card']");
        if (tc == null) return Task.FromResult(RuleResult.Fail("Brak Twitter Cards", "Brak tagu twitter:card.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Twitter Cards", "Znaleziono Twitter Cards."));
    }
}
