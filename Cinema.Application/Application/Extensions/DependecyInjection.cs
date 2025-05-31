﻿using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Application.Application.Extensions;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
            
        /*// Регистрируем MediatR
        services.AddMediatR(Assembly.GetExecutingAssembly());
            
        // Регистрируем AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // Регистрируем ICustomMapper
        services.AddScoped<ICustomMapper, CustomMapper>();*/

        return services;
    }
}