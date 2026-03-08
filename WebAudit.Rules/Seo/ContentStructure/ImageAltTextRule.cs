using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.ContentStructure;

public class ImageAltTextRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var images = context.Document.DocumentNode.SelectNodes("//img[not(@alt) or normalize-space(@alt)='']");
        if (images != null && images.Count > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak tekstów alternatywnych (Alt text)",
                description: $"Na Twojej stronie zlokalizowaliśmy {images.Count} grafik bez opisu alternatywnego (atrybut 'alt'). Jest on kluczowy w budowaniu widoczności obrazów w Google Images, które stanowi niemal jedną czwartą całego ruchu organicznego wyszukiwarki. Dodatkowo ten błąd łamie fundamentalne zasady dostępności cyfrowej dla osób niewidomych, pogarszając ogólną punktację od wiodących platform.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Naprawimy błędy w Twoich grafikach, wypełniając dla Ciebie wyselekcjonowane słowa kluczowe we wszystkie opisy alt. Dzięki takiemu zabiegowi zdjęcia na Twojej stronie same zaczną sprowadzać do Ciebie bezpłatny, wysokiej jakości ruch ze strony Google Images!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawne teksty alternatywne (Alt text)",
            description: "Wszystkie obrazy na stronie <img> posiadają wypełnione opisy (atrybuty alt), wspierające pozycjonowanie i poprawiające dostępność witryny. Wyśmienita praca!"
        ));
    }
}