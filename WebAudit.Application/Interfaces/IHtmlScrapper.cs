using WebAudit.Domain.ValueObjects;

namespace WebAudit.Application.Interfaces;

public interface IHtmlScraper
{
    Task<AuditContext> ScrapeAsync(Uri url);
}