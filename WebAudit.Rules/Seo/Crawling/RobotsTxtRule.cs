using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.Crawling;

public class RobotsTxtRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Crawling;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        // Reguła będzie wymagała oddzielnego strzału HTTP
        return Task.FromResult(RuleResult.Skip());
    }
}