using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security;

public class HttpToHttpsRedirectRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.TargetUrl.Scheme == "http") return Task.FromResult(RuleResult.Fail("Brak HTTPS", "Strona ładuje się po HTTP zamiast HTTPS.", SeverityLevel.Critical, ""));
        return Task.FromResult(RuleResult.Pass("HTTPS", "Strona używa HTTPS."));
    }
}
