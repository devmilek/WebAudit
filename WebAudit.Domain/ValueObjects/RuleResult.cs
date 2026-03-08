using WebAudit.Domain.Enums;

namespace WebAudit.Domain.ValueObjects;

public record RuleResult(
    RuleStatus Status,
    string Title,
    string Description,
    SeverityLevel? Severity = null,
    string? AgencyPitch = null) 
{
    public static RuleResult Pass(string title, string description) 
        => new(RuleStatus.Passed, title, description);

    public static RuleResult Fail(string title, string description, SeverityLevel severity, string agencyPitch) 
        => new(RuleStatus.Failed, title, description, severity, agencyPitch);
    
    public static RuleResult Skip() 
        => new(RuleStatus.Skipped, string.Empty, string.Empty);
}