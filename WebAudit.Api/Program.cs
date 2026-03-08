using Scalar.AspNetCore;
using WebAudit.Application.Engine;
using WebAudit.Application.Interfaces;
using WebAudit.Domain.Interfaces;
using WebAudit.Infrastructure.Scraping;
using WebAudit.Rules;
using WebAudit.Rules.Seo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IHtmlScraper, PuppeteerHtmlScraper>();
builder.Services.AddScoped<AuditEngine>();
builder.Services.AddAllAuditRules();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
