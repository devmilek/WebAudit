using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class HeaderHierarchyRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        // Uproszczona logika
        return Task.FromResult(RuleResult.Skip());
    }
}
