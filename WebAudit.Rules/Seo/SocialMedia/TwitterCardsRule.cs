using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.SocialMedia;

public class TwitterCardsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.SocialMedia;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var tc = context.Document.DocumentNode.SelectSingleNode("//meta[@name='twitter:card']");
        if (tc == null)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak zdefiniowanego Twitter Cards",
                description: "Nie znaleźliśmy głównego tagu 'twitter:card'. System Twitter (obecnie platforma X) w ten sposób autoryzuje i przetwarza linki udostępniane w aplikacji, na tablicach domowych czy w direct message'ach. Pominięcie tego fragmentu skazuje Twoje odnośniki na ślepy lub niewymiarowy wycinek, tracąc potężny zastrzyk angażującej wielokolorowej przestrzeni u konsumentów i pogarszając spływający zasięg organiczny z Twittera/X.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Stworzymy dla Ciebie niesamowite i zachwycające podsumowania wizytówek dla platformy X. Kiedy Twój produkt, artykuł czy portal pojawi się w retwitach - przykuje niepodzielną uwagę milionów dając mu zoptymalizowaną graficzną potęgę kliknięć."
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawne tagi Twitter Cards",
            description: "Gratulacje, znaleziono deklaracje Twitter Cards. Moduły zoptymalizowane pod tę sieć społecznościową gwarantują maksymalne zasięgi wyświetleń oraz estetyczny kadr linkowanego url'a na osi czasu!"
        ));
    }
}