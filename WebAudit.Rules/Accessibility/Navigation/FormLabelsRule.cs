using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Accessibility.Navigation;

public class FormLabelsRule : IAuditRule
{
    public Category Category => Category.Accessibility;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Navigation;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var inputNodes = context.Document.DocumentNode.SelectNodes("//input[not(@type='hidden') and not(@type='submit') and not(@type='button')] | //textarea | //select");
        int unlabelledCount = 0;

        if (inputNodes != null)
        {
            foreach (var input in inputNodes)
            {
                var id = input.GetAttributeValue("id", string.Empty);
                var hasAriaLabel = !string.IsNullOrEmpty(input.GetAttributeValue("aria-label", string.Empty)) || !string.IsNullOrEmpty(input.GetAttributeValue("aria-labelledby", string.Empty));
                var hasTitle = !string.IsNullOrEmpty(input.GetAttributeValue("title", string.Empty));

                var labelExists = false;
                if (!string.IsNullOrEmpty(id))
                {
                    labelExists = context.Document.DocumentNode.SelectSingleNode($"//label[@for='{id}']") != null;
                }

                // Jeśli element wejściowy znajduje się wewnątrz elementu label
                var isInsideLabel = input.ParentNode != null && input.ParentNode.Name.ToLower() == "label";

                if (!labelExists && !hasAriaLabel && !hasTitle && !isInsideLabel)
                {
                    unlabelledCount++;
                }
            }
        }

        if (unlabelledCount > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak zdefiniowanych etykiet pól formularzy",
                description: $"Na Twoich formularzach kontaktowych i procesie płatności zidentyfikowaliśmy {unlabelledCount} pól, dla których nie dołączono logicznych etykiet (<label> lub aria-label). To nie tylko brak profesjonalizmu - systemy wspomagające z których korzystają osoby niepełnosprawne, powiadomią ich tylko, że widzą pole wpisywania, nie informując ich o czym jest, co skutecznie zapobiegnie ich rejestracji, opłaceniu zamówienia w kasie, czy zapytaniu ofertowemu.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Przeprowadzimy dogłębny audyt dostępności WCAG dla Twoich procedur sprzedaży (Check-Out) oraz formularzy wsparcia klienta. Narysujemy dla Twoich niedowidzących klientów jasną mapę prowadzącą do bezproblemowych zakupów!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawne etykiety formularzy",
            description: "Doskonale. Wykryliśmy poprawnie połączone etykiety z polami kontaktowymi. Twoja marka promuje i gwarantuje inkluzywność cyfrową dla wszystkich."
        ));
    }
}