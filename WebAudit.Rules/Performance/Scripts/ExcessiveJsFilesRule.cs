using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.Scripts;

public class ExcessiveJsFilesRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Scripts;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var scripts = context.Document.DocumentNode.SelectNodes("//script[@src]");
        if (scripts != null && scripts.Count > 5)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Za dużo plików JS",
                description: $"Znaleziono {scripts.Count} zewnętrznych skryptów JS. Każdy kolejny plik JavaScript to kolejne zapytanie sieciowe oraz koszt parsowania, kompilacji i wykonania, co drastycznie obniża czas interaktywności strony dla użytkownika.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Przeprowadzimy głęboki audyt i połączymy Twoje pliki JavaScript w mniejszą liczbę wysoce zoptymalizowanych paczek. Sprawimy, że Twoja strona będzie reagować na kliknięcia od razu po załadowaniu!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Optymalna liczba plików JS",
            description: "Liczba plików JS jest w normie (5 lub mniej). Zmniejsza to czas blokowania głównego wątku przeglądarki, pozwalając na szybkie rozpoczęcie interakcji z użytkownikiem."
        ));
    }
}