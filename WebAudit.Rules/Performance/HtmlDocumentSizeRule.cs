using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance;

public class HtmlDocumentSizeRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.AssetsSize;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.RawHtml.Length > 150 * 1024) return Task.FromResult(RuleResult.Fail("Zbyt duży plik HTML", $"Rozmiar pliku HTML to {context.RawHtml.Length / 1024} KB (zalecane max 150 KB).", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Rozmiar pliku HTML", "Rozmiar pliku HTML jest w normie."));
    }
}
