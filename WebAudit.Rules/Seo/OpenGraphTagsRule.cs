using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class OpenGraphTagsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.OpenGraph;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var ogTitle = context.Document.DocumentNode.SelectSingleNode("//meta[@property='og:title']");
        var ogDesc = context.Document.DocumentNode.SelectSingleNode("//meta[@property='og:description']");
        if (ogTitle == null || ogDesc == null) return Task.FromResult(RuleResult.Fail("Brak tagów OG", "Brak tagów og:title lub og:description.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Tagi OG", "Znaleziono podstawowe tagi OG."));
    }
}
