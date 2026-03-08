using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Security.ServerHeaders;

public class ServerInfoLeakRule : IAuditRule
{
    public Category Category => Category.Security;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ServerHeaders;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null)
        {
            if (context.Headers.Contains("X-Powered-By") || context.Headers.Contains("Server"))
            {
                return Task.FromResult(RuleResult.Fail(
                    title: "Wyciek informacji o technologiach serwerowych",
                    description: "W odpowiedziach serwera znajdowane są nagłówki 'X-Powered-By' lub 'Server'. Udostępniają one bez cenzury informacje o tym, jakiego serwera używasz (np. nginx, Apache, IIS) oraz na jakich frameworkach stoi cała logika (np. ASP.NET, PHP). To niepotrzebny wgląd w zaplecze techniczne serwisu, i często otwarte zaproszenie dla wyszukiwarek podatności w określonych wersjach oprogramowania.",
                    severity: SeverityLevel.Warning,
                    agencyPitch: "Dokonamy sterylizacji zwracanych odpowiedzi, utajniając wszystkie wyciekające dane ze źródła, i pozbawiając złośliwe boty narzędzi do skanowania Twojej firmy."
                ));
            }
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Brak wycieków informacji z nagłówków",
            description: "Serwer nie zwraca demaskujących nagłówków 'X-Powered-By' i 'Server', maskując użyte technologie operacyjne i dodając ważną warstwę 'security by obscurity'."
        ));
    }
}