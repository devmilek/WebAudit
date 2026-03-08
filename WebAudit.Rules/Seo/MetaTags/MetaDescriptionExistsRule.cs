using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.MetaTags;

public class MetaDescriptionExistsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.MetaTags;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var descriptionNode = context.Document.DocumentNode.SelectSingleNode("//meta[@name='description']");
        var content = descriptionNode?.GetAttributeValue("content", string.Empty).Trim();

        if (string.IsNullOrEmpty(content))
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak zdefiniowanego Meta Description",
                description: "Strona nie posiada zdefiniowanego tagu meta description w sekcji <head>. Wtedy przeglądarki same dobierają fragment strony z dowolnego miejsca, co często jest całkowicie przypadkowe (np. ukryty cennik z menu lub stopka), zamazując Twój marketingowy komunikat reklamowy.",
                severity: SeverityLevel.High,
                agencyPitch: "Stworzymy dla Ciebie mistrzowskie, perswazyjne opisy z wyselekcjonowanymi, silnie rotacyjnymi słowami kluczowymi, co bezbłędnie chwyci uwagę Twoich przyszłych klientów przeglądających pierwszą stronę wyszukiwarki Google i natychmiast klikną w link!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawny Meta Description",
            description: $"Wyszukiwarka znalazła odpowiednio dobrany i spersonalizowany tag meta description: '{content}'."
        ));
    }
}