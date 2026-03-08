using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security.ServerHeaders;

public class PermissionsPolicyRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null)
        {
            if (!context.Headers.Contains("Permissions-Policy") && !context.Headers.Contains("Feature-Policy"))
            {
                return Task.FromResult(RuleResult.Fail(
                    title: "Brak zdefiniowanego Permissions-Policy",
                    description: "Twój serwer nie ogranicza dostępu stronom trzecim przez dyrektywy nagłówka 'Permissions-Policy'. Bez tej definicji, reklamy, niebezpieczne wtyczki reklamowe i inne zasoby zagnieżdżone, mają z zasady niczym nieograniczony dostęp do użycia funkcji urządzenia (jak obciążenie Twojego mikrofonu, sprawdzenie lokalizacji z modułu GPS Twojego smartfonu, lub kamery internetowej użytkownika) tak długo jak gościsz ich zasoby we własnym serwisie. To olbrzymi wyłom prawny dla prywatności.",
                    severity: SeverityLevel.Warning,
                    agencyPitch: "Przeprowadzimy precyzyjne zamknięcie i ograniczenie dostępu (lockdown) przez konfigurację Twojej infrastruktury serwerowej! Pożegnaj wścibskie wycieki w reklamach. Powiedz 'stop' i weź oddech wolności, zapewniając bezpieczne środowisko bez ryzyka dla Twoich lojalnych klientów!"
                ));
            }
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Poprawny nagłówek Permissions-Policy",
            description: "Doskonale. Zdefiniowana zasada 'Permissions-Policy' to nowoczesna obrona przed zewnętrznymi wtyczkami, które nadużywają uprawnień sprzętowych bez zgody twórcy strony."
        ));
    }
}