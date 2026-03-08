using System.Net.Http.Headers;
using HtmlAgilityPack;

namespace WebAudit.Domain.ValueObjects;

public class AuditContext
{
    public Uri TargetUrl { get; }
    public string RawHtml { get; }
    public HttpResponseHeaders? Headers { get; } // Dodane nagłówki
    public TimeSpan LoadTime { get; }

    private readonly Lazy<HtmlDocument> _parsedDocument;
    public HtmlDocument Document => _parsedDocument.Value;

    // Zwróć uwagę na nazwy parametrów w konstruktorze (zaczynają się z małej litery)
    public AuditContext(Uri targetUrl, string rawHtml, HttpResponseHeaders? headers, TimeSpan loadTime)
    {
        TargetUrl = targetUrl ?? throw new ArgumentNullException(nameof(targetUrl));
        RawHtml = rawHtml ?? string.Empty;
        Headers = headers;
        LoadTime = loadTime;

        _parsedDocument = new Lazy<HtmlDocument>(() => 
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(RawHtml);
            return doc;
        });
    }
}