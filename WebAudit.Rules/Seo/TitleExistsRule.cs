using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;

namespace WebAudit.Rules.Seo;

public class TitleExistsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        // Proste sprawdzenie stringa (później zastąpisz to biblioteką HtmlAgilityPack)
        var titleNode = context.Document.DocumentNode.SelectSingleNode("//head/title");
        var titleText = titleNode?.InnerText.Trim();

        if (string.IsNullOrEmpty(titleText))
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak tagu <title>",
                description: "Strona nie posiada zdefiniowanego tytułu w sekcji <head>.",
                severity: SeverityLevel.Critical,
                agencyPitch: "Dodamy zoptymalizowane pod słowa kluczowe tagi <title> na wszystkich Twoich podstronach, aby podbić Twoją pozycję w Google."
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Tag <title> jest poprawny.", 
            description: $"Znaleziono tytuł: '{titleText}'"
        ));
    }
}