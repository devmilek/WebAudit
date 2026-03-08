using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security;

public class ContentTypeOptionsRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null) {
            if (!context.Headers.Contains("X-Content-Type-Options")) {
                return Task.FromResult(RuleResult.Fail("Brak X-Content-Type-Options", "Brak nagłówka X-Content-Type-Options: nosniff.", SeverityLevel.Warning, ""));
            }
        }
        return Task.FromResult(RuleResult.Pass("X-Content-Type-Options", "Znaleziono nagłówek X-Content-Type-Options."));
    }
}
