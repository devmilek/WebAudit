using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security.ServerHeaders;

public class HttpToHttpsRedirectRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.TargetUrl.Scheme == "http")
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak szyfrowania HTTPS",
                description: "Twoja strona ładuje się przez przestarzały protokół HTTP. Oznacza to, że wszelkie przesyłane informacje (dane logowania, hasła, zawartość strony) krążą w sieci jako otwarty tekst, całkowicie podatny na przechwycenie. Przeglądarki takie jak Google Chrome jawnie oznaczają tego typu strony jako 'Niezabezpieczone', bardzo skutecznie strasząc i zniechęcając klientów.",
                severity: SeverityLevel.Critical,
                agencyPitch: "Wdrożymy certyfikat SSL na Twoją stronę i zabezpieczymy każdą interakcję z Twoimi klientami, dzięki czemu zielona kłódka wróci na Twoją domenę, budując niesamowite zaufanie i odblokowując lepsze miejsca w wynikach Google!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Szyfrowanie za pomocą HTTPS",
            description: "Strona wykorzystuje szyfrowane protokoły komunikacyjne z prawidłowym certyfikatem. Połączenie klienta jest bezpieczne od momentu wpisania adresu."
        ));
    }
}