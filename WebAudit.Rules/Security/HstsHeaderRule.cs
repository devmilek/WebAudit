using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security;

public class HstsHeaderRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null && context.TargetUrl.Scheme == "https") {
            if (!context.Headers.Contains("Strict-Transport-Security")) {
                return Task.FromResult(RuleResult.Fail("Brak HSTS", "Brak nagłówka Strict-Transport-Security.", SeverityLevel.High, ""));
            }
        }
        return Task.FromResult(RuleResult.Pass("HSTS", "Znaleziono nagłówek HSTS."));
    }
}
