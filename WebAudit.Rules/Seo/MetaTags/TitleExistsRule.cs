using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.MetaTags;

public class TitleExistsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var titleNode = context.Document.DocumentNode.SelectSingleNode("//head/title");
        var titleText = titleNode?.InnerText.Trim();

        if (string.IsNullOrEmpty(titleText))
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak zdefiniowanego tagu <title>",
                description: "Strona nie posiada zdefiniowanego tytułu w sekcji <head>. Tytuł strony (zadeklarowany w tagu <title>) jest absolutnie pierwszym, a zarazem najbardziej fundamentalnym i widocznym sygnałem dla algorytmów Google oceniającym to, o czym opowiada podstrona. Brak takiego tytułu powoduje natychmiastową utratę widoczności strony dla wyszukiwarek a w kartach przeglądarki użytkownicy ujrzą zamiast nazwy strony, suchy adres Twojego URL'u, budząc podejrzenia o oszustwo (phishing).",
                severity: SeverityLevel.Critical,
                agencyPitch: "Przeprowadzimy dedykowane badania intencji Twoich odbiorców po czym dodamy zoptymalizowane pod słowa kluczowe tagi <title> na wszystkich Twoich podstronach! To najlepsze co można zrobić by pchnąć Twoją firmę na wyższe pozycje w rankingu wyszukiwań od samej góry lejka zakupowego!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Tag <title> istnieje na stronie",
            description: $"Rewelacja! Znaleziono profesjonalnie napisany i zadeklarowany atrybut <title>: '{titleText}' wspierający widoczność Twojego biznesu."
        ));
    }
}