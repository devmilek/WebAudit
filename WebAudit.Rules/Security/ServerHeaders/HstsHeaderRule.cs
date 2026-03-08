using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security.ServerHeaders;

public class HstsHeaderRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null && context.TargetUrl.Scheme == "https")
        {
            if (!context.Headers.Contains("Strict-Transport-Security"))
            {
                return Task.FromResult(RuleResult.Fail(
                    title: "Brak Strict-Transport-Security (HSTS)",
                    description: "Twoja strona ładuje się przez bezpieczne połączenie (HTTPS), ale serwer nie egzekwuje tego u wszystkich klientów korzystając z nagłówka HSTS (Strict-Transport-Security). Bez niego witryna wciąż może być zaatakowana przy pierwszej próbie odwiedzin (atak Man-in-the-Middle) poprzez sztuczne zdegradowanie połączenia do starszego, niezabezpieczonego HTTP.",
                    severity: SeverityLevel.High,
                    agencyPitch: "Włączymy twarde wymuszanie protokołu HTTPS (HSTS) na Twoim serwerze. Sprawimy, że nawet najbardziej wyrafinowane ataki podsłuchujące komunikację w Twoim sklepie odbiją się jak od ściany!"
                ));
            }
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Aktywna ochrona HSTS",
            description: "Znaleziono nagłówek 'Strict-Transport-Security'. To znaczy, że serwer wymusza łączenie się z witryną wyłącznie przez bezpieczny, szyfrowany protokół przez zadeklarowany czas."
        ));
    }
}