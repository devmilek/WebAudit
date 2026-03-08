using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.MetaTags;

public class MetaDescriptionLengthRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var descriptionNode = context.Document.DocumentNode.SelectSingleNode("//meta[@name='description']");
        var content = descriptionNode?.GetAttributeValue("content", string.Empty).Trim();

        if (string.IsNullOrEmpty(content))
        {
            return Task.FromResult(RuleResult.Skip());
        }

        if (content.Length < 50 || content.Length > 160)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Zła długość Meta Description",
                description: $"Długość tagu meta description ({content.Length} znaków) nie mieści się w zalecanym przedziale (50-160 znaków). Jeśli będzie za długi, to tekst reklamy w Google zostanie nieestetycznie przerwany z dodaniem wielokropka w kluczowym punkcie (tzw. ucięty opis). Jeżeli będzie za krótki, to zmarnujesz znakomite darmowe pole reklamowe dla swojej witryny, przez co spadnie Twoja pozycja (niższy wskaźnik konwersji).",
                severity: SeverityLevel.Warning,
                agencyPitch: "Przeprowadzimy precyzyjne dopasowanie i reedycję zawartości każdego kluczowego fragmentu opisu. Wydobędziemy wszystko, co najlepsze, sprawiając, że każdy z elementów reklamy domknie sprzedaż dla użytkowników wyszukiwarek, nie tracąc ani centymetra widoczności!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Optymalna długość Meta Description",
            description: $"Rewelacja! Twój meta description mieści się w idealnych rozmiarach ({content.Length} znaków na zalecane 50-160 znaków), świetnie dopasowany do listowania w mobilnym oraz desktopowym silniku wyszukiwarek (np. Bing i Google)."
        ));
    }
}