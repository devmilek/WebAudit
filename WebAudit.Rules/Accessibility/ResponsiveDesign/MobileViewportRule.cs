using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Accessibility.ResponsiveDesign;

public class MobileViewportRule : IAuditRule
{
    public Category Category => Category.Accessibility;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ResponsiveDesign;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var node = context.Document.DocumentNode.SelectSingleNode("//meta[@name='viewport']");
        if (node == null)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak zdefiniowanego viewportu mobilnego",
                description: "Nie wykryliśmy odpowiedniego tagu <meta name=\"viewport\">. Brak tego elementu powoduje, że urządzenia mobilne będą wyświetlały pomniejszoną wersję strony na komputery, zmuszając użytkowników do nienaturalnego powiększania ekranu i drastycznie pogarszając UX na smartfonach.",
                severity: SeverityLevel.Critical,
                agencyPitch: "Dostosujemy Twoją witrynę, aby idealnie reagowała i skalowała się na wszystkich telefonach i tabletach. Zapewnimy nowoczesne i wygodne doświadczenie Twoim mobilnym klientom, którzy stanowią dziś większość ruchu w internecie!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawnie zdefiniowany Viewport",
            description: "Strona posiada odpowiednią deklarację tagu <meta name=\"viewport\">. Pomaga on poprawnie poinformować urządzenia mobilne, w jaki sposób mają powiększać i skalować przeglądaną platformę internetową."
        ));
    }
}