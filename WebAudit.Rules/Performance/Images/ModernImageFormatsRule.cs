using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.Images;

public class ModernImageFormatsRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Images;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var images = context.Document.DocumentNode.SelectNodes("//img[contains(@src, '.jpg') or contains(@src, '.png') or contains(@src, '.jpeg')]");
        if (images != null && images.Count > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Niezoptymalizowane starsze formaty obrazów",
                description: $"Na stronie znaleziono {images.Count} obrazków o rozszerzeniach .jpg, .jpeg lub .png. Chociaż te formaty są popularne, to znacząco ustępują nowoczesnym standardom kompresji (np. WebP, AVIF), przez co bez potrzeby obciążają łącze i spowalniają pobieranie multimediów przez użytkowników.",
                severity: SeverityLevel.Info,
                agencyPitch: "Przekonwertujemy wszystkie Twoje obrazki na format nowej generacji, jak WebP czy AVIF, co zagwarantuje ułamkowe czasy wczytywania i sprawi, że Google pokocha Twoją stronę z zachowaniem pięknej jakości każdego zdjęcia!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Nowoczesne formaty graficzne",
            description: "Wspaniale! Nie znaleźliśmy żadnych ciężkich, przestarzałych formatów (jak .jpg czy .png). Zastosowanie lekkich, współczesnych formatów świetnie przyspiesza całą platformę."
        ));
    }
}