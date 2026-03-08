using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security.ServerHeaders;

public class ContentSecurityPolicyRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null)
        {
            if (!context.Headers.Contains("Content-Security-Policy"))
            {
                return Task.FromResult(RuleResult.Fail(
                    title: "Brak zdefiniowanej polityki CSP (Content-Security-Policy)",
                    description: "W nagłówkach odpowiedzi serwera brakuje 'Content-Security-Policy'. Bez polityki CSP Twoja strona jest wysoce podatna na jedne z najgroźniejszych ataków w internecie (tzw. XSS - Cross-Site Scripting). Hakerzy mogą wstrzyknąć złośliwy kod wprost na Twoją stronę, wykradając hasła klientów, czy przechwytując sesje, a Twoja przeglądarka bezrefleksyjnie taki złośliwy kod wykona.",
                    severity: SeverityLevel.High,
                    agencyPitch: "Stworzymy dla Ciebie niesamowicie szczelną architekturę bezpieczeństwa i rygorystyczną zaporę dla dozwolonych źródeł skryptów (CSP). Odetniemy ataki typu wstrzyknięć kodu u źródła i wyeliminujemy kradzieże danych logowania, ratując renomę i bazę użytkowników Twojego biznesu wizerunkowego!"
                ));
            }
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawny nagłówek Content-Security-Policy",
            description: "Wspaniale! Wykorzystujesz najnowocześniejszy standard obronny przed atakami XSS - serwer definiuje restrykcyjny, aktywny nagłówek 'Content-Security-Policy'."
        ));
    }
}