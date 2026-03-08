namespace WebAudit.Domain.Enitites;

public class AuditReport
{
    public Guid Id { get; private set; } // Główne ID raportu
    public string TargetUrl { get; private set; }
    public string ClientEmail { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public int OverallScore { get; private set; }
    
    private readonly List<AuditIssue> _issues = new();
    public IReadOnlyCollection<AuditIssue> Issues => _issues.AsReadOnly();

    public AuditReport(string targetUrl, string clientEmail)
    {
        Id = Guid.NewGuid();
        TargetUrl = targetUrl;
        ClientEmail = clientEmail;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddIssue(AuditIssue issue)
    {
        _issues.Add(issue);
    }

    // W przyszłości dodasz tu logikę wyliczającą punktację, np.:
    // public void CalculateScore() { ... }
}