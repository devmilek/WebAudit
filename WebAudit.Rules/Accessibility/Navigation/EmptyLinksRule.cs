using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Accessibility.Navigation;

public class EmptyLinksRule : IAuditRule
{
    public Category Category => Category.Accessibility;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Navigation;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var links = context.Document.DocumentNode.SelectNodes("//a[not(normalize-space()) and not(*[normalize-space()]) and not(@aria-label) and not(@aria-labelledby)]");
        if (links != null && links.Count > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Puste linki nawigacyjne bez tekstu",
                description: $"Na stronie jest {links.Count} hiperłączy, które nie posiadają czytelnego tekstu, atrybutu aria-label ani aria-labelledby. Osoby korzystające z czytników ekranowych (np. niewidomi), napotykając pusty link nie wiedzą, dokąd on prowadzi, przez co mogą uznać stronę za zepsutą i szybko ją opuścić.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Naprawimy błędy w Twoich odnośnikach, czyniąc Twoją stronę przejrzystą i w 100% dostępną dla osób niepełnosprawnych, dzięki czemu spełnisz wyśrubowane standardy WCAG i wzbudzisz większe zaufanie Google."
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Brak pustych linków",
            description: "Wszystkie Twoje linki <a> posiadają przypisany do nich, zdefiniowany tekst lub atrybut czytelny dla nawigacji asystującej (czytników ekranowych)."
        ));
    }
}