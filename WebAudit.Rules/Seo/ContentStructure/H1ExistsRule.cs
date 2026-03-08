using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.ContentStructure;

public class H1ExistsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var h1Nodes = context.Document.DocumentNode.SelectNodes("//h1");
        if (h1Nodes == null || h1Nodes.Count == 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak nagłówka <h1>",
                description: "Na stronie całkowicie brakuje znacznika głównego nagłówka <h1>. To fundamentalny błąd w strukturze i jeden z najpotężniejszych wyznaczników widoczności organicznej SEO z całego tekstu dokumentu strony. Znacznik <h1> służy wyszukiwarce jako główne potwierdzenie i teza zawartości artykułu. Gdy z powodu braku H1 wyszukiwarki nie rozumieją kontekstu portalu, omija to od kilku do kilkuset tysięcy słów kluczowych każdego dnia.",
                severity: SeverityLevel.High,
                agencyPitch: "Dodamy i przeredagujemy mocne, intencyjne nagłówki poziomu <h1> na absolutnie każdej z Twoich podstron! Nasączymy te słowa intencją, obniżając koszty kampanii i stawiając Cię przed Twoją uboższą i starszą konkurencją!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawny nagłówek <h1>",
            description: "Strona posiada znacznik <h1> i używa hierarchii tak, jak lubią maszyny czytające i asystenci."
        ));
    }
}