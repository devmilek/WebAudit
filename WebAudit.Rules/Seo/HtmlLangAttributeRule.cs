using WebAudit.Domain.Enums;
using WebAudit.Domain.Interfaces;
using WebAudit.Domain.ValueObjects;
using System.Threading.Tasks;

namespace WebAudit.Rules.Seo;

public class HtmlLangAttributeRule : IAuditRule
{
    public Category Category => Category.Seo;
    public SubCategory? SubCategory => Domain.Enums.SubCategory.ContentStructure;
    
    public Task<RuleResult> ExecuteAsync(AuditContext context)
    {
        var htmlNode = context.Document.DocumentNode.SelectSingleNode("//html");
        var lang = htmlNode?.GetAttributeValue("lang", string.Empty);
        if (string.IsNullOrEmpty(lang)) return Task.FromResult(RuleResult.Fail("Brak atrybutu lang", "Tag html nie ma atrybutu lang.", SeverityLevel.Warning, ""));
        return Task.FromResult(RuleResult.Pass("Atrybut lang", $"Znaleziono atrybut lang: {lang}"));
    }
}
