using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance;

public class ExcessiveCssFilesRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.AssetsSize;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var css = context.Document.DocumentNode.SelectNodes("//link[@rel='stylesheet']");
        if (css != null && css.Count > 5) return Task.FromResult(RuleResult.Fail("Za dużo plików CSS", $"Zbyt duża liczba plików CSS: {css.Count}.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Pliki CSS", "Liczba plików CSS jest w normie."));
    }
}
