using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Accessibility;

public class EmptyLinksRule : IAuditRule
{
    public Category Category => Category.Accessibility;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Navigation;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var links = context.Document.DocumentNode.SelectNodes("//a[not(normalize-space()) and not(*[normalize-space()]) and not(@aria-label)]");
        if (links != null && links.Count > 0) return Task.FromResult(RuleResult.Fail("Puste linki", $"Znaleziono {links.Count} pustych linków bez tekstu i aria-label.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Puste linki", "Brak pustych linków."));
    }
}
