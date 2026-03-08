using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security.ServerHeaders;

public class ContentTypeOptionsRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null)
        {
            if (!context.Headers.Contains("X-Content-Type-Options"))
            {
                return Task.FromResult(RuleResult.Fail(
                    title: "Brak nagłówka X-Content-Type-Options",
                    description: "Serwer nie zwraca nagłówka 'X-Content-Type-Options: nosniff'. Brak tego nagłówka pozwala przeglądarkom na tzw. MIME-sniffing, czyli domyślanie się typu przesyłanych plików. Może to zostać wykorzystane przez hakerów do ominięcia zabezpieczeń strony poprzez ukrycie niebezpiecznych skryptów wewnątrz niewinnie wyglądających plików.",
                    severity: SeverityLevel.Warning,
                    agencyPitch: "Zabezpieczymy Twój serwer dodając brakujące nagłówki ochrony MIME, co całkowicie zablokuje wektor ataków przez podejrzane pliki wstrzykiwane w Twoją witrynę!"
                ));
            }
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawny nagłówek X-Content-Type-Options",
            description: "Twój serwer posiada nagłówek 'X-Content-Type-Options: nosniff', chroniąc stronę przed szkodliwym zgadywaniem typów plików przez przeglądarki użytkowników."
        ));
    }
}