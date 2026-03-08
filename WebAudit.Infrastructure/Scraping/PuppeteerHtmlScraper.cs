using System.Diagnostics;
using PuppeteerSharp;
using WebAudit.Application.Interfaces;
using WebAudit.Domain.ValueObjects;

namespace WebAudit.Infrastructure.Scraping;

public class PuppeteerHtmlScraper : IHtmlScraper
{
    private readonly HttpClient _httpClient;

    public PuppeteerHtmlScraper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<AuditContext> ScrapeAsync(Uri url)
    {
        var stopwatch = Stopwatch.StartNew();

        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        
        var launchOptions = new LaunchOptions
        {
            Headless = true,
            Args = new[] { "--no-sandbox", "--disable-setuid-sandbox", "--disable-gpu" } 
        };
        
        await using var browser = await Puppeteer.LaunchAsync(launchOptions);
        await using var page = await browser.NewPageAsync();
        
        await page.SetViewportAsync(new ViewPortOptions { Width = 1920, Height = 1080 });
        
        var response = await page.GoToAsync(url.ToString(), new NavigationOptions
        {
            WaitUntil = new[] { WaitUntilNavigation.Networkidle2 },
            Timeout = 30000
        });
        
        string renderedHtml = await page.GetContentAsync();
        
        stopwatch.Stop();
        
        var headers = response.Headers;
        
        return new AuditContext(url, renderedHtml, null, stopwatch.Elapsed);
    }
}