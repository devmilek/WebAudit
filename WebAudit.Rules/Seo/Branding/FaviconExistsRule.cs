using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo.Branding;

public class FaviconExistsRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.Branding;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var favicon = context.Document.DocumentNode.SelectSingleNode("//link[contains(@rel, 'icon')]");
        if (favicon == null)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Brak zdefiniowanej Favicony",
                description: "Nie znaleziono odnośnika do ikony Favicon w sekcji <head>. Favicona to ta mała ikonka widoczna na kartach w przeglądarce oraz coraz częściej w wynikach wyszukiwania mobilnego Google. Jej brak to niższy wskaźnik klikalności (CTR) przez użytkowników, którzy omijają mniej profesjonalnie wyglądające strony, i ogólna strata wizerunkowa dla marki.",
                severity: SeverityLevel.Info,
                agencyPitch: "Zaprojektujemy i podepniemy profesjonalną, dopasowaną Faviconę dla Twojego serwisu. Wzmocnimy zaufanie odwiedzających oraz sprawimy, że Twoja marka zacznie rzucać się w oczy bezpośrednio na liście wyników wyszukiwania w smartfonach!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Favicona jest poprawna",
            description: "Strona posiada poprawnie zadeklarowaną ikonę Favicon. Użytkownicy łatwo rozpoznają Twoją zakładkę w przeglądarce!"
        ));
    }
}