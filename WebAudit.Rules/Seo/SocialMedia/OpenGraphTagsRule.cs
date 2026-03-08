using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.SocialMedia;

public class OpenGraphTagsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.SocialMedia;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var ogTags = context.Document.DocumentNode.SelectNodes("//meta[starts-with(@property, 'og:')]");
        if (ogTags == null || ogTags.Count == 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak tagów OpenGraph",
                description: "Nie zlokalizowaliśmy zdefiniowanych atrybutów OpenGraph (og:title, og:description, og:url, og:type). Ich brak powoduje potężne zamieszanie na portalach takich jak LinkedIn czy Facebook przy zaciąganiu danych o wklejonym przez użytkownika odnośniku, bo portale społecznościowe pobiorą na chybił trafił to co znajdą. Oznacza to ogromny uszczerbek w profesjonalnym i wycelowanym w potencjalnego konsumenta, komunikacie w mediach społecznościowych.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Przeprowadzimy precyzyjne dopasowanie meta atrybutów dla standardu OpenGraph na wszystkie podstrony z bloga, sklepu i aktualności, co bezpośrednio poskutkuje pięknymi miniaturami do powiększania zaangażowania Twojej grupy odbiorczej."
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawne tagi OpenGraph",
            description: $"Wyszukiwarka zlokalizowała {ogTags.Count} znaczników OpenGraph dopasowujących treść platformy do komunikatorów społecznościowych."
        ));
    }
}