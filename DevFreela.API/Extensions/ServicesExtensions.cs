using Asp.Versioning;
using DevFreela.API.Configurations;
using DevFreela.Application.Commands.ProjectCommands.CreateProject;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators.Project;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DevFreela.API.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddApplicationServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração essencial para APIs
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            // Response customizada para erros no model state (validações) status code 400 (bad request)
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value!.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value!.Errors.Select(err => err.ErrorMessage).ToArray()
                    );

                return new BadRequestObjectResult(new
                {
                    Title = "Validation errors",
                    Details = "One or more validation errors occurred",
                    Status = StatusCodes.Status400BadRequest,
                    Path = context.HttpContext.Request.Path.Value,
                    Errors = errors
                });
            };
        });
        services.AddEndpointsApiExplorer();

        // Injeção de dependências
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IProjectRepository, ProjectRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ISkillRepository, SkillRepository>();

        services.AddScoped<IProjectService, ProjectService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ISkillService, SkillService>();

        // Padrão Option com configurações
        services.Configure<OpeningTimeOption>(configuration.GetSection("OpeningTime"));


        return services;
    }

    public static IServiceCollection AddValidationsConfiguration(this IServiceCollection services)
    {
        // Validações com FluentValidation
        services.AddFluentValidationAutoValidation();
        // Validação por classe
        // services.AddScoped<IValidator<CreateProjectCommand>, CreateProjectCommandValidator>();
        // Validação Geral
        services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>();

        return services;
    }

    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        // Configurando MediatR
        services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining(typeof(CreateProjectCommand)));
        return services;
    }

    public static IServiceCollection AddRoutesConfiguration(this IServiceCollection services)
    {
        // Converte rotas para letras minúsculas
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        return services;
    }

    public static IServiceCollection AddVersioningConfiguration(this IServiceCollection services)
    {
        // Configurações de versionamento
        services.AddApiVersioning(opts =>
        {
            opts.DefaultApiVersion = new ApiVersion(2, 0); // Versão padrão
            opts.AssumeDefaultVersionWhenUnspecified = true; // Assume a versão padrão quando não especificada
            opts.ReportApiVersions = true; // Reporta as versões da API
            opts.ApiVersionReader = new UrlSegmentApiVersionReader(); // Lê a versão da API a partir do segmento da URL
        }).AddApiExplorer(opts => // Adiciona o suporte ao Swagger
        {
            opts.GroupNameFormat = "'v'V"; // Formato de exibição das versões
            opts.SubstituteApiVersionInUrl = true; // Substitui a versão da API na URL
        });

        return services;
    }
}
