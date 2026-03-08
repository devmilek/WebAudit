using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.MetaTags;

public class CanonicalTagRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var canonical = context.Document.DocumentNode.SelectSingleNode("//link[@rel='canonical']");
        if (canonical == null)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak tagu Canonical",
                description: "Nie znaleźliśmy linku z atrybutem rel='canonical'. Kiedy Twoja strona jest dostępna pod kilkoma adresami (np. www i bez www, lub z parametrami śledzącymi), Google może potraktować to jako duplikację treści, co znacząco obniża pozycję strony w wynikach wyszukiwania.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Dodamy do wszystkich Twoich podstron poprawny tag Canonical, dzięki któremu Google jednoznacznie rozpozna główną wersję Twoich treści. Zapomnij o obniżonych wynikach z powodu duplikacji – skup całą 'moc' SEO na jednym, głównym adresie!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Tag Canonical jest poprawny",
            description: "Znaleziono deklarację tagu canonical. Wspaniale, pomaga to wyszukiwarkom precyzyjnie indeksować wyznaczoną stronę!"
        ));
    }
}