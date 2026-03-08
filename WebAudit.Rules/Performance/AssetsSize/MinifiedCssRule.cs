using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.AssetsSize;

public class MinifiedCssRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.AssetsSize;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var cssLinks = context.Document.DocumentNode.SelectNodes("//link[@rel='stylesheet' and @href]");
        int unminifiedCount = 0;

        if (cssLinks != null)
        {
            foreach (var link in cssLinks)
            {
                var href = link.GetAttributeValue("href", string.Empty);
                if (!href.Contains(".min.css") && href.EndsWith(".css"))
                {
                    unminifiedCount++;
                }
            }
        }

        if (unminifiedCount > 0)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Niezminifikowane arkusze stylów (CSS)",
                description: $"Na Twojej stronie znaleźliśmy {unminifiedCount} plików CSS o standardowym rozszerzeniu (bez końcówki .min.css). Zazwyczaj oznacza to, że kod nie został zminifikowany. Pliki CSS pełne białych znaków, komentarzy twórców i spacji tworzą nielogicznie zawyżoną wagę. Przeglądarka internetowa u Twojego klienta musi pobrać tysiące zbędnych znaków opóźniając start renderowania i podbijając zużycie gigabajtów i energii baterii w telefonie.",
                severity: SeverityLevel.Warning,
                agencyPitch: "Dodamy do Twojej ścieżki wdrożeniowej profesjonalne, bezstratne kompresory paczek CSS (minifikację), miażdżąc wagę stron nawet o 70%, co drastycznie obniży opóźnienie wywoływania każdej Twojej podstrony w oczach Google i niecierpliwych mobilnych internautów!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Pliki CSS są poprawnie skompresowane",
            description: "Wspaniale! Wykryliśmy ślady wskazujące, że udostępniasz zminifikowane zasoby arkuszy stylów (.min.css). Świadczy to o znakomitej kondycji Twojego systemu optymalizacji dostarczanych pakietów strony."
        ));
    }
}