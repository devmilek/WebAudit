namespace WebAudit.Rules;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WebAudit.Domain.Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection AddAllAuditRules(this IServiceCollection services)
    {
        var rulesAssembly = Assembly.GetExecutingAssembly();
        
        var ruleTypes = rulesAssembly.GetTypes()
            .Where(t => t.IsClass 
                        && !t.IsAbstract 
                        && typeof(IAuditRule).IsAssignableFrom(t));
        
        foreach (var type in ruleTypes)
        {
            services.AddTransient(typeof(IAuditRule), type);
        }
        
        return services;
    }
}