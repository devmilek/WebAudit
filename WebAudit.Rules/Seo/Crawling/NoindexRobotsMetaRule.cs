using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.Crawling;

public class NoindexRobotsMetaRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Crawling;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var robotsMeta = context.Document.DocumentNode.SelectSingleNode("//meta[@name='robots']");
        var content = robotsMeta?.GetAttributeValue("content", string.Empty).ToLower();

        if (content != null && (content.Contains("noindex") || content.Contains("nofollow")))
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Wykryto blokadę indeksowania (noindex / nofollow)",
                description: $"Twoja strona posiada znacznik meta robots o wartości '{content}'. Oznacza to, że celowo nakazuje ona wyszukiwarkom, takim jak Google, aby całkowicie zignorowały jej istnienie i nie dodawały jej do wyników wyszukiwania. W środowisku produkcyjnym jest to błąd krytyczny i natychmiastowe odcięcie niemal 100% darmowego ruchu z sieci. Czasami takie tagi zostają przypadkowo po fazie testów i budowy strony.",
                severity: SeverityLevel.Critical,
                agencyPitch: "Przeprowadzimy dogłębny skan architektury Twojej domeny, wykrywając i odblokowując wszystkie zamknięte z niewiedzy wrota do Twoich zysków. Pozwolimy gigantom takim jak Google wreszcie dostrzec i docenić Twój produkt!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Strona otwarta na indeksowanie",
            description: "Nie znaleziono tagów meta robots z dyrektywami noindex ani nofollow. Algorytmy wyszukiwarek mają swobodny dostęp, by dodawać Twoje treści do rankingu!"
        ));
    }
}