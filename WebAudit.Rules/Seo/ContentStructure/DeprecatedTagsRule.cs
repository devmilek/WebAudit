using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.ContentStructure;

public class DeprecatedTagsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var deprecatedTags = context.Document.DocumentNode.SelectNodes("//font | //center | //marquee | //blink | //strike | //u");

        if (deprecatedTags != null && deprecatedTags.Count > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Wykryto przestarzałe znaczniki HTML (Deprecated Tags)",
                description: $"Wykryliśmy na Twojej stronie kodowanie za pomocą starych, bezwzględnie usuniętych z oficjalnej technologii znaczników wizualnych, takich jak '<font>', '<center>' czy innych elementów z początków internetu. Wykryto {deprecatedTags.Count} takich przypadków. Kod źródłowy jest czytany jako bardzo przestarzały, bardzo ubogi jakościowo dla najnowszych mechanizmów wyszukiwarki (Google Core Web Vitals) i budzący lęk o bezpieczeństwo całego systemu na którym działa usługa.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Przeprowadzimy brutalną przebudowę logiki oraz przestarzałych elementów stylowych pod spodem Twojego systemu. Zmienimy stary kod w nieskazitelny, wysoce zoptymalizowany dla najnowszych czytników językiem HTML5. Rzuci to całkowicie nowe, i zdrowe jakościowo, spojrzenie gigantów na punktację domeny i jej rzetelność!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Brak starych, przestarzałych tagów HTML",
            description: "Wspaniale, nie wykazaliśmy na Twojej stronie reliktów strukturalnych (np. font, center), Twój kod operuje na współczesnej wersji (np. wykorzystuje arkusze CSS) i gwarantuje pewne czytanie przez najnowsze urządzenia."
        ));
    }
}