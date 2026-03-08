using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance;

public class RenderBlockingJsRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Scripts;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var scripts = context.Document.DocumentNode.SelectNodes("//head/script[@src and not(@defer) and not(@async)]");
        if (scripts != null && scripts.Count > 0) return Task.FromResult(RuleResult.Fail("Blokujące skrypty JS", $"Znaleziono {scripts.Count} skryptów blokujących w sekcji <head>.", SeverityLevel.High, ""));
        return Task.FromResult(RuleResult.Pass("Blokujące skrypty JS", "Brak blokujących skryptów JS w sekcji <head>."));
    }
}
