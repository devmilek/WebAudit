using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security.ServerHeaders;

public class XFrameOptionsRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null)
        {
            if (!context.Headers.Contains("X-Frame-Options"))
            {
                return Task.FromResult(RuleResult.Fail(
                    title: "Podatność Clickjacking (Brak X-Frame-Options)",
                    description: "Serwer nie ustawia nagłówka 'X-Frame-Options', który zapobiega możliwości zagnieżdżenia Twojej strony przez atakujących w iframe'ach na swoich, oszukańczych serwisach (tzw. Clickjacking). To kluczowa funkcja. Użytkownicy mogą kliknąć w spreparowany w ten sposób link, myśląc, że to inna, znana akcja, czym mogą przelać środki finansowe lub niechcący skasować cenne konto z Twojej witryny.",
                    severity: SeverityLevel.High,
                    agencyPitch: "Skompletujemy profesjonalną tarczę dla Twojej witryny na wszystkich serwerach, dołączając nagłówki, które przerwą ten schemat i uniemożliwią nakładanie na Twoje usługi wizerunku wrogich stron!"
                ));
            }
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Odporność na ataki typu Clickjacking (X-Frame-Options)",
            description: "Nagłówek 'X-Frame-Options' istnieje i broni dostępu przez nieupoważnione osadzanie, dając Ci w pełni odporność na wektor z atakami w warstwie iframe'ów."
        ));
    }
}