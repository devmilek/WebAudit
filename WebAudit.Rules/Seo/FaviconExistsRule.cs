using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class FaviconExistsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Branding;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var node = context.Document.DocumentNode.SelectSingleNode("//link[@rel='icon' or @rel='shortcut icon']");
        if (node == null) return Task.FromResult(RuleResult.Fail("Brak favicon", "Nie znaleziono tagu favicon.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Favicon", "Znaleziono favicon."));
    }
}
