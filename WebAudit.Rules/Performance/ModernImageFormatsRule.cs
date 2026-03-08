using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance;

public class ModernImageFormatsRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Images;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var images = context.Document.DocumentNode.SelectNodes("//img[contains(@src, '.jpg') or contains(@src, '.png')]");
        if (images != null && images.Count > 0) return Task.FromResult(RuleResult.Fail("Stare formaty obrazów", $"Znaleziono {images.Count} obrazków w starych formatach (.jpg, .png).", SeverityLevel.Info, ""));
        return Task.FromResult(RuleResult.Pass("Formaty obrazów", "Brak obrazków w starych formatach."));
    }
}
