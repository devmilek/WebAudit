using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.Images;

public class LazyLoadingImagesRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Images;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var images = context.Document.DocumentNode.SelectNodes("//img[not(@loading='lazy')]");
        if (images != null && images.Count > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak leniwego ładowania (lazy loading)",
                description: $"Znaleźliśmy {images.Count} obrazków, które pobierane są od razu po wejściu na stronę, niezależnie od tego, czy użytkownik do nich przewinie. To drastycznie zwiększa rozmiar początkowego ładowania, zużycie transferu oraz obniża wydajność strony (tzw. Largest Contentful Paint).",
                severity: SeverityLevel.Warning,
                agencyPitch: "Dodamy do Twojej strony mechanizmy leniwego ładowania, dzięki którym grafiki pojawią się dopiero wtedy, gdy użytkownik będzie gotowy na nie spojrzeć! Ograniczy to zacinanie i odblokuje szybkie ładowanie się strony internetowej."
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawne wykorzystanie Lazy Loading",
            description: "Wszystkie Twoje obrazki korzystają z leniwego ładowania lub strona w ogóle ich nie posiada. Obrazy nie opóźniają początkowego wyświetlania widoku."
        ));
    }
}