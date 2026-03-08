using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.SocialMedia;

public class OpenGraphImageRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.SocialMedia;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var ogImage = context.Document.DocumentNode.SelectSingleNode("//meta[@property='og:image']");
        if (ogImage == null)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak zdefiniowanego obrazu OpenGraph (og:image)",
                description: "Nie wykryto w meta-danych odnośnika do obrazu z atrybutem og:image. To szalenie ważne. Gdy potencjalny klient podzieli się linkiem na Messengerze, LinkedInie, WhatsAppie lub Facebooku, zamiast przyciągającej oko grafiki otrzymają jedynie wygenerowane 'na ślepo' pole. Drastycznie zabija to skuteczność reklamy wirusowej, oraz powoduje niższy stopień rzucania się w oczy klikalnych odnośników w tablicy aktualności.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Dodamy potężne narzędzie wizualnej komunikacji – spersonalizowane grafiki og:image wybijające Twoją stronę we wszystkich mediach społecznościowych. To automatyczny, potężny darmowy branding oraz gigantyczny współczynnik (CTR) klikalności przy każdym poleceniu linku!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawny znacznik OpenGraph image",
            description: "Brawo! Posiadasz poprawnie zdefiniowaną ikonę oraz powiązane odnośniki w tagu og:image. Twoje publikowane strony przyciągną znacznie więcej wzroku!"
        ));
    }
}