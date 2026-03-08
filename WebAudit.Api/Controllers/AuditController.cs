using Microsoft.AspNetCore.Mvc;
using WebAudit.Api.Contracts.Requests;
using WebAudit.Application.Engine;

namespace WebAudit.Api.Controllers;

[ApiController]
[Route("api/audit-controller")]
public class AuditController : ControllerBase
{
    private readonly AuditEngine _auditEngine;
    
    public AuditController(AuditEngine auditEngine)
    {
        _auditEngine = auditEngine;
    }
    
    [HttpPost]
    [Route("/audit-website")]
    public async Task<IActionResult> AuditWebsite([FromBody] AuditRequest auditRequest)
    {
        try
        {
            var report = await _auditEngine.RunAuditAsync(auditRequest.Url, auditRequest.Email);

            return Ok(report);
        }
        catch (Exception ex)
        {
            return Problem($"Nie udało się przeprowadzić audytu: {ex.Message}");
        }
    }
}