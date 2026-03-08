using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.MetaTags;

public class TitleLengthRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var titleNode = context.Document.DocumentNode.SelectSingleNode("//head/title");
        var titleText = titleNode?.InnerText.Trim();

        if (string.IsNullOrEmpty(titleText))
        {
            return Task.FromResult(RuleResult.Skip());
        }

        if (titleText.Length < 10 || titleText.Length > 60)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Niewłaściwa długość tagu <title>",
                description: $"Długość tagu tytułowego strony wynosi {titleText.Length} znaków. Wytyczne Google zalecają utrzymanie zawartości w limicie od 10 do około 60 znaków by zapobiec tzw. urywanym tytułom (z wielokropkiem) w sekcjach mobilnego listowania, lub utracie widoczności kluczowych fraz. Gdy Twój potencjalny klient szuka Twojego produktu wpisując frazę, ucinanie reszty zdania powoduje lawinowy spadek konwersji (niższy CTR) na wejścia.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Odnajdziemy właściwy, hipnotyzujący wzorzec, wyciskając sto procent mocy perswazji, zachowując limitowaną długość we wszystkich tagach tytułowych Twojej platformy e-commerce lub portfela B2B. Pozycje same podskoczą."
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Optymalna długość tagu <title>",
            description: $"Znakomicie skonstruowany tytuł! Mieści się on w najbardziej optymalnych przedziałach, idealnie dawkując objętość dla ekranów najnowszych smartfonów: '{titleText}' ({titleText.Length} znaków)."
        ));
    }
}