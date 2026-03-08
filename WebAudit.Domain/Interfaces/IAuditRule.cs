using WebAudit.Domain.Enums;
using WebAudit.Domain.ValueObjects;

namespace WebAudit.Domain.Interfaces;

public interface IAuditRule
{
    // Kategoria ułatwi potem grupowanie wyników w PDF
    Category Category { get; }
    
    SubCategory? SubCategory { get; }
    
    // Główna metoda skanująca, przyjmuje kontekst (HTML) i zwraca wynik
    Task<RuleResult> ExecuteAsync(AuditContext context);
}