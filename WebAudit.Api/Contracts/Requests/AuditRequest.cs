using System.ComponentModel.DataAnnotations;

namespace WebAudit.Api.Contracts.Requests;

public class AuditRequest
{
    [Required]
    [Url]
    public string Url { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}