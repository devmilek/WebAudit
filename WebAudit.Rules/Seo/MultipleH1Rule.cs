using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class MultipleH1Rule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var h1Nodes = context.Document.DocumentNode.SelectNodes("//h1");
        if (h1Nodes != null && h1Nodes.Count > 1) return Task.FromResult(RuleResult.Fail("Wiele tagów H1", $"Znaleziono {h1Nodes.Count} tagów H1. Powinien być jeden.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Tagi H1", "Znaleziono poprawną liczbę tagów H1."));
    }
}
