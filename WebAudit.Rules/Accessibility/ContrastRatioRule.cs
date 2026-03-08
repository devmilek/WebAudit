using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Accessibility;

public class ContrastRatioRule : IAuditRule
{
    public Category Category => Category.Accessibility;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Visual;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        // TODO: Wymaga wyrenderowania (np. Puppeteer) do oceny stylów obliczonych
        return Task.FromResult(RuleResult.Skip());
    }
}
