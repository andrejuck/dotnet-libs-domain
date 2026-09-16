using System.Reflection;
using Libs.Domain.DomainEvent;
using Microsoft.Extensions.DependencyInjection;

namespace Libs.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.AddDomainEventDispatcher();
        var handlerInterfaceType = typeof(IDomainEventHandler<>);

        var handlerImplementations = assembly.GetTypes()
            .Where(type => !type.IsAbstract && !type.IsInterface)
            
            .SelectMany(type => type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                .Select(i => new { Interface = i, Implementation = type }));

        foreach (var handler in handlerImplementations)
            services.AddScoped(handler.Interface, handler.Implementation);

        return services;
    }
    
    public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
        return services;
    }
}