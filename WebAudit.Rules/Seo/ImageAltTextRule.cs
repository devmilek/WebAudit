using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using WebAudit.Domain.Enums;
namespace WebAudit.Rules.Seo;

public class ImageAltTextRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Images;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var images = context.Document.DocumentNode.SelectNodes("//img");
        
        if (images == null || images.Count == 0)
        {
            return Task.FromResult(RuleResult.Pass("Teksty alternatywne (ALT)", "Brak obrazków na stronie."));
        }
        
        var imagesWithoutAlt = images.Where(img => 
            string.IsNullOrWhiteSpace(img.GetAttributeValue("alt", string.Empty))
        ).ToList();
        
        if (imagesWithoutAlt.Any())
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brakujące teksty alternatywne (ALT) obrazków",
                description: $"Znaleźliśmy {imagesWithoutAlt.Count} obrazków bez atrybutu ALT (na ogólną liczbę {images.Count}). To szkodzi Twojemu SEO i dostępności dla osób niewidomych.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Uzupełnimy tagi ALT dla wszystkich grafik w Twojej witrynie, co pomoże Ci pojawić się w wynikach wyszukiwania."
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Teksty alternatywne (ALT)", 
            description: $"Wszystkie {images.Count} obrazki mają poprawne tagi ALT."
        ));
    }
}