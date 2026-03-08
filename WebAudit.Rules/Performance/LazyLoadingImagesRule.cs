using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance;

public class LazyLoadingImagesRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Images;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var images = context.Document.DocumentNode.SelectNodes("//img[not(@loading='lazy')]");
        if (images != null && images.Count > 0) return Task.FromResult(RuleResult.Fail("Brak lazy loading", $"Znaleziono {images.Count} obrazków bez loading='lazy'.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Lazy loading", "Wszystkie obrazki mają lazy loading lub brak obrazków."));
    }
}
