using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.AssetsSize;

public class MinifiedJsRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.AssetsSize;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var jsScripts = context.Document.DocumentNode.SelectNodes("//script[@src]");
        int unminifiedCount = 0;

        if (jsScripts != null)
        {
            foreach (var script in jsScripts)
            {
                var src = script.GetAttributeValue("src", string.Empty);
                if (!src.Contains(".min.js") && src.EndsWith(".js"))
                {
                    unminifiedCount++;
                }
            }
        }

        if (unminifiedCount > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Niezminifikowane skrypty (JS)",
                description: $"Na Twojej witrynie znaleziono {unminifiedCount} nieprzetworzonych, 'surowych' plików skryptowych JS (bez przedrostka .min.js). Skrypty to jedna z najcięższych klas komponentów jakie przeglądarka poddaje analizie przy uruchamianiu, bo wymagają najpierw przeparsowania w maszynie v8 a dopiero po tym - wykonania. Niepotrzebne ciągi długich znaków i spacji potężnie paraliżują (zamrażają) telefon na kilka sekund przed jakąkolwiek interakcją.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Oczyścimy absolutnie wszystkie zasoby Java Script ze zjawiska rozdęcia (tzw. code bloat), wpinając dla Twoich witryn agresywną kompresję gzip. Odmrozimy w ten sposób czas ładowania (TTI), a każda z klikniętych ikon na stronie wreszcie zacznie błyskawicznie odpowiadać!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Pliki skryptów (JS) są poprawnie skompresowane",
            description: "Wspaniale! Znaleźliśmy odpowiednio skompresowane logiczne pliki Javascript po atrybutach .min.js. Serwer drastycznie oszczędza na darmowej i bezpiecznej optymalizacji paczek zasobów logicznych strony."
        ));
    }
}