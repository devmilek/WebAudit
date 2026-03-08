using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Performance.AssetsSize;

public class TextCompressionRule : IAuditRule
{
    public Category Category => Category.Performance;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.AssetsSize;

    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        if (context.Headers != null)
        {
            if (!context.Headers.Contains("Content-Encoding"))
            {
                return Task.FromResult(RuleResult.Fail(
                    title: "Brak kompresji tekstu na poziomie serwera (GZIP/Brotli)",
                    description: "Serwer tej witryny wysyła kod HTML, CSS i Javascript w niezmniejszonej i całkowicie czystej formie (brak nagłówka Content-Encoding sugerującego np. algorytm gzip lub br). To jak wysyłanie listów pocztą, wypisując po jednym zdaniu na każdą z milionów kartek. Twoi klienci muszą pobierać z serwera kolosalne, zbędne zera i jedynki dla każdego elementu wizualnego na smartfonie, pożerając baterie i transfer komórkowy.",
                    severity: SeverityLevel.High,
                    agencyPitch: "Natychmiastowo aktywujemy na Twoich maszynach potężne, najnowocześniejsze standardy kompresji strumienia Brotli. Rozmiażdżymy czas wczytywania, tnąc go nawet pięciokrotnie, tworząc doświadczenie responsywności natywnej aplikacji na każdym z urządzeń!"
                ));
            }
        }

        return Task.FromResult(RuleResult.Pass(
            title: "Serwer stosuje kompresję tekstu (GZIP/Brotli)",
            description: "Brawo! Wykorzystujesz nagłówek Content-Encoding co oznacza że serwer zmniejsza objętość przed lotem pliku do użytkownika minimalizując koszty, oraz znacznie redukując wagę."
        ));
    }
}