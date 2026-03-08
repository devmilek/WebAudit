using WebAudit.Application.Interfaces;
using WebAudit.Domain.Enitites;
using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;

namespace WebAudit.Application.Engine;

public class AuditEngine
{
    private readonly IHtmlScraper _scraper;
    private readonly IEnumerable<IAuditRule> _rules;
    
    public AuditEngine(IHtmlScraper scraper, IEnumerable<IAuditRule> rules)
    {
        _scraper = scraper;
        _rules = rules;
    }
    
    public async Task<AuditReport> RunAuditAsync(string targetUrl, string clientEmail)
    {
        var uri = new Uri(targetUrl);
        
        var context = await _scraper.ScrapeAsync(uri);

        var report = new AuditReport(targetUrl, clientEmail);

        foreach (var rule in _rules)
        {
            var result = await rule.ExecuteAsync(context);
            
            if (result.Status != RuleStatus.Skipped)
            {
                var issue = new AuditIssue(rule.Category, result);
                report.AddIssue(issue);
            }
        }
        
        return report;
    }
}