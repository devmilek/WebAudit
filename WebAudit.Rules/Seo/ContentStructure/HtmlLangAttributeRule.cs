using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.ContentStructure;

public class HtmlLangAttributeRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var htmlNode = context.Document.DocumentNode.SelectSingleNode("//html");
        var lang = htmlNode?.GetAttributeValue("lang", string.Empty);

        if (string.IsNullOrEmpty(lang))
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak zdefiniowanego atrybutu lang",
                description: "Główny znacznik <html> Twojej strony nie posiada atrybutu 'lang'. Atrybut ten precyzuje w jakim języku jest napisana strona (np. lang='pl'). Jego brak lub błędna deklaracja ogromnie utrudnia asystentom głosowym i narzędziom takim jak Google Translate poprawną interpretację strony, a także odbiera punkty do regionalnego SEO.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Dodamy pełne deklaracje językowe dopasowane do wszystkich rynków na których operujesz (lang oraz atrybuty hreflang). Zapewnimy tym, że wyszukiwarki precyzyjnie przydzielą Ci odpowiednich klientów z Twojego kraju lub regionu docelowego!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawny atrybut Lang",
            description: $"Strona używa poprawnego atrybutu lang: '{lang}'. Wyszukiwarki oraz czytniki ekranowe doskonale wiedzą w jakim języku jest podawana treść!"
        ));
    }
}