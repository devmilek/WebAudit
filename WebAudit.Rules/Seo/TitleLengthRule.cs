using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;

namespace WebAudit.Rules.Seo;

public class TitleLengthRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;
    
    private const int MinLength = 30; 
    private const int MaxLength = 60; 

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var titleNode = context.Document.DocumentNode.SelectSingleNode("//head/title");
        var titleText = titleNode?.InnerText.Trim();

        if (string.IsNullOrEmpty(titleText))
        {
            return Task.FromResult(RuleResult.Pass(
                title: "Pomijam sprawdzanie długości tytułu", 
                description: "Tytuł nie istnieje na stronie."
            ));
        }

        if (titleText.Length < MinLength || titleText.Length > MaxLength)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Nieoptymalna długość tagu <title>",
                description: $"Obecna długość to {titleText.Length} znaków. Zalecana długość to między {MinLength} a {MaxLength} znaków.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Nasi copywriterzy zoptymalizują Twoje tytuły, aby idealnie mieściły się w oknie wyszukiwarki i przyciągały więcej kliknięć (wyższe CTR)."
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Długość tagu <title> jest poprawna.", 
            description: $"Tytuł ma {titleText.Length} znaków, co mieści się w limitach."
        ));
    }
}