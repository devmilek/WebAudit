using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Accessibility.Navigation;

public class ButtonHasTextRule : IAuditRule
{
    public Category Category => Category.Accessibility;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Navigation;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var buttons = context.Document.DocumentNode.SelectNodes("//button[not(normalize-space()) and not(*[normalize-space()]) and not(@aria-label) and not(@aria-labelledby) and not(@title)]");

        if (buttons != null && buttons.Count > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Przyciski bez czytelnej etykiety lub tekstu",
                description: $"Znaleziono {buttons.Count} przycisków typu <button>, które ukrywają się za pustymi z punktu widzenia maszyn ikonkami, bez zdefiniowanego jakiegokolwiek własnego opisu tekstowego, tytułu, czy chociażby specjalistycznego atrybutu (aria-label). To kluczowy obszar użyteczności strony (Accessibility) i element standardów WCAG. Bez opisanych przycisków, np. osoby niedowidzące korzystające z syntezatorów głosu zamiast odczytać 'Kup teraz' lub 'Wyślij formularz', usłyszą u siebie komunikat 'przycisk, bez etykiety', po czym bezradnie i natychmiast opuszczą witrynę.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Naprawimy i wdrożymy standardy WCAG (AA) na wszystkich polach akcji, w Twoim koszyku sprzedażowym, we wszystkich formularzach rejestracji i kontaktach. Otworzysz wrota na dziesiątki tysięcy zaniedbanych w sieci, a gotowych płacić, użytkowników internetu!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawnie zadeklarowane przyciski",
            description: "Doskonale zorganizowane wszystkie główne elementy do interakcji dla użytkowników (np. atrybuty title / text przy tagach <button>). Każdy asystent głosu świetnie obsłuży tak zaprojektowane menu czy koszyk!"
        ));
    }
}