using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance;

public class ExcessiveJsFilesRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Scripts;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var scripts = context.Document.DocumentNode.SelectNodes("//script[@src]");
        if (scripts != null && scripts.Count > 5) return Task.FromResult(RuleResult.Fail("Za dużo plików JS", $"Zbyt duża liczba plików JS: {scripts.Count}.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Pliki JS", "Liczba plików JS jest w normie."));
    }
}
