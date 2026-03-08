using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security.ServerHeaders;

public class ReferrerPolicyRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null)
        {
            if (!context.Headers.Contains("Referrer-Policy"))
            {
                return Task.FromResult(RuleResult.Fail(
                    title: "Brak nagłówka Referrer-Policy",
                    description: "Serwer Twojej platformy internetowej nie zwraca nagłówka 'Referrer-Policy', decydującego o ukrywaniu Twojego adresu (z poufnymi danymi sesji, lub tajnymi tokenami w ścieżce url) przed serwerami zewnętrznymi (tzw. wyciek informacji Referer). Umożliwiasz tym obcym stronom do których linkujesz podgląd wrażliwych adresów swoich podstron.",
                    severity: SeverityLevel.Warning,
                    agencyPitch: "Naprawimy i precyzyjnie zamaskujemy całą drogę wejściową Twoich konsumentów dodając silny moduł Referrer-Policy. Ochronisz przez to cenne informacje i odzyskasz poufność analityki Twojego wewnętrznego lejka sprzedaży i paneli administracyjnych!"
                ));
            }
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawny nagłówek Referrer-Policy",
            description: "Brawo. Strona chroni dane poprzez stosowanie nagłówka 'Referrer-Policy', zapobiegając niepotrzebnym wyciekom wrażliwych wewnętrznych ścieżek dostępowych z url do serwisów trzecich."
        ));
    }
}