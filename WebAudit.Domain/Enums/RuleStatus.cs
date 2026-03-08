namespace WebAudit.Domain.Enums;

public enum RuleStatus
{
    Passed,     // Test zdany
    Failed,     // Test oblany (błąd na stronie)
    Skipped     // Test pominięty (brak warunków do uruchomienia)
}