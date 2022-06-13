﻿using System.Reflection;
using CleanCompanyName.DDDMicroservice.Application.Pipelines;
using CleanCompanyName.DDDMicroservice.Application.Common.Mappers;
using CleanCompanyName.DDDMicroservice.Domain.Entities.Products.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCompanyName.DDDMicroservice.Application;



public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionEnrichingPipelineBehaviour<,>));

        MapperConfig.AddMappingConfigs();

        return services;
    }
}