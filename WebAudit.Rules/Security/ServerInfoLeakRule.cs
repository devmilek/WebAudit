using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security;

public class ServerInfoLeakRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null) {
            if (context.Headers.Contains("X-Powered-By") || context.Headers.Contains("Server")) {
                return Task.FromResult(RuleResult.Fail("Wyciek informacji o serwerze", "Wykryto nagłówki X-Powered-By lub Server.", SeverityLevel.Warning, ""));
            }
        }
        return Task.FromResult(RuleResult.Pass("Informacje o serwerze", "Brak wycieku informacji o serwerze."));
    }
}
