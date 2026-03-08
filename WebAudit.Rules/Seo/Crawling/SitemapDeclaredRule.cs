using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.Crawling;

public class SitemapDeclaredRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Crawling;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        // Najczęściej sprawdza się to w robots.txt
        return Task.FromResult(RuleResult.Skip());
    }
}