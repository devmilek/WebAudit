using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class OpenGraphImageRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.OpenGraph;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var ogImg = context.Document.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
        if (ogImg == null) return Task.FromResult(RuleResult.Fail("Brak obrazka OG", "Brak tagu og:image.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Obrazek OG", "Znaleziono obrazek OG."));
    }
}
