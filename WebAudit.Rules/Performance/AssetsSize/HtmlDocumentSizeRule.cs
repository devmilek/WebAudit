using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.AssetsSize;

public class HtmlDocumentSizeRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.AssetsSize;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var sizeInKb = context.RawHtml.Length / 1024;

        if (sizeInKb > 150)
        {
            return Task.FromResult(RuleResult.Fail(
                title: "Zbyt duży dokument HTML",
                description: $"Rozmiar Twojego pliku HTML to {sizeInKb} KB, co przekracza zalecane maksimum 150 KB. Wielki plik HTML spowalnia działanie przeglądarki na etapie pobierania oraz budowania struktury DOM. Często oznacza to niepotrzebne wstawianie zbyt wielkiej ilości stylów bezpośrednio w kod (inline).",
                severity: SeverityLevel.Warning,
                agencyPitch: "Oczyścimy Twój kod z niepotrzebnego balastu, zminifikujemy tagi, i odciążymy stronę. Czysty kod to natychmiastowe ładowanie dla użytkowników o słabszym połączeniu z siecią!"
            ));
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Optymalny rozmiar HTML",
            description: $"Rozmiar Twojego dokumentu HTML ({sizeInKb} KB) jest w bezpiecznej normie. Przeglądarka będzie w stanie bardzo szybko przetworzyć dokument i przygotować podstawy widoku strony dla użytkownika."
        ));
    }
}