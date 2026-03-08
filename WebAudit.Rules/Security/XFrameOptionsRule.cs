using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security;

public class XFrameOptionsRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null) {
            if (!context.Headers.Contains("X-Frame-Options")) {
                return Task.FromResult(RuleResult.Fail("Brak X-Frame-Options", "Brak nagłówka X-Frame-Options chroniącego przed Clickjackingiem.", SeverityLevel.High, ""));
            }
        }
        return Task.FromResult(RuleResult.Pass("X-Frame-Options", "Znaleziono nagłówek X-Frame-Options."));
    }
}
