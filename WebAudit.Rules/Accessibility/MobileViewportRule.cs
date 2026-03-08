using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Accessibility;

public class MobileViewportRule : IAuditRule
{
    public Category Category => Category.Accessibility;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ResponsiveDesign;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var node = context.Document.DocumentNode.SelectSingleNode("//meta[@name='viewport']");
        if (node == null) return Task.FromResult(RuleResult.Fail("Brak viewport", "Brak tagu meta viewport.", SeverityLevel.Critical, ""));
        return Task.FromResult(RuleResult.Pass("Viewport", "Znaleziono tag meta viewport."));
    }
}
