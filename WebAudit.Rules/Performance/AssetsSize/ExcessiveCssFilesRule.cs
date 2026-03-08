using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.AssetsSize;

public class ExcessiveCssFilesRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.AssetsSize;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var css = context.Document.DocumentNode.SelectNodes("//link[@rel='stylesheet']");
        if (css != null && css.Count > 5)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Za dużo plików CSS",
                description: $"Znaleziono {css.Count} plików CSS. Zbyt duża liczba żądań o pliki stylów spowalnia ładowanie strony, ponieważ przeglądarka musi wykonać osobne zapytanie HTTP dla każdego pliku, co blokuje renderowanie.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Zoptymalizujemy Twoją stronę łącząc i minifikując pliki CSS. Dzięki temu Twoja witryna będzie ładować się błyskawicznie, co poprawi konwersję i pozycję w Google!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Optymalna liczba plików CSS",
            description: "Liczba plików CSS jest w normie (5 lub mniej). To bardzo dobrze wpływa na szybkość ładowania strony, ograniczając niepotrzebne zapytania sieciowe."
        ));
    }
}