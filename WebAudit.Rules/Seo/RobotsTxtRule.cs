using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class RobotsTxtRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Crawling;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        // TODO: Wymaga oddzielnego zapytania HTTP
        return Task.FromResult(RuleResult.Skip());
    }
}
