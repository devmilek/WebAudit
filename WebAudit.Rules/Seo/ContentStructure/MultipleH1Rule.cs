using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.ContentStructure;

public class MultipleH1Rule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var h1Nodes = context.Document.DocumentNode.SelectNodes("//h1");
        if (h1Nodes != null && h1Nodes.Count > 1)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Wielokrotny znacznik <h1>",
                description: $"Na Twojej podstronie znajduje się aż {h1Nodes.Count} nagłówków <h1>. Klasyczne, pozycjonowanie w Google zaleca stosowanie jednego unikalnego i głównego nagłówka <h1> dla całej podstrony, określającego ogólną zawartość tematyczną, ponieważ to wokół niego algorytm ocenia adekwatność materiału. Przeładowanie strony tym atrybutem rozmywa sygnały SEO.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Przeprowadzimy czyszczenie składniowej semantyki całego dokumentu HTML na Twoim portalu. Przywrócimy spójną hierarchię nagłówkową i wzmocnimy jedyny słuszny temat docelowy. Rozwiązując ten dylemat pomożemy maszynom błyskawicznie zrozumieć Twoją treść i pokierować falą wejść wprost z Google!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Pojedynczy główny nagłówek <h1>",
            description: "Gratulacje, znaleziono poprawną liczbę nagłówków <h1> (jeden). W ten sposób główna teza i hasło witryny zostanie jednoznacznie połączone z wyszukaną w sieci informacją."
        ));
    }
}