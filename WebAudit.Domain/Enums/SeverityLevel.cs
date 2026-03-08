namespace WebAudit.Domain.Enums;

public enum SeverityLevel
{
    Info,       // Drobnostka, warto poprawić (np. minifikacja CSS)
    Warning,    // Problem średniej wagi (np. brak atrybutów alt)
    High,       // Poważny problem (np. bardzo wolny czas ładowania)
    Critical    // Tragedia (np. wygasły certyfikat SSL, blokada indeksowania w robots.txt)
}