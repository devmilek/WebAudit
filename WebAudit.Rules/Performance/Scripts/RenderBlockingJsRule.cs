using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.Scripts;

public class RenderBlockingJsRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Scripts;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var scripts = context.Document.DocumentNode.SelectNodes("//head/script[@src and not(@defer) and not(@async)]");
        if (scripts != null && scripts.Count > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Skrypty blokujące renderowanie (Render Blocking)",
                description: $"Wykryliśmy {scripts.Count} skryptów JS w sekcji <head>, które nie posiadają atrybutów defer lub async. Przeglądarka widząc takie pliki, dosłownie przerywa rysowanie treści na ekranie użytkownika, dopóki ich w pełni nie pobierze i nie wykona. To potężne, przestarzałe obciążenie.",
                severity: SeverityLevel.High,
                agencyPitch: "Odetkamy blokady ładowania Twojej platformy dodając nowoczesne techniki wstrzykiwania skryptów. Uwolnimy Twoją stronę przed niekończącymi się sekundami pustego, białego ekranu dla oczekujących użytkowników!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Brak skryptów blokujących renderowanie (Render Blocking)",
            description: "Doskonale zoptymalizowane skrypty z wykorzystaniem odpowiednich atrybutów ładują się bez opóźniania podstawowych struktur wyświetlania użytkownikowi widoku strony (brak plików w <head> bez async/defer)."
        ));
    }
}