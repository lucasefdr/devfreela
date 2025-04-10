using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace DevFreela.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        // Configuração do Swagger
        services.AddSwaggerGen(options =>
        {
            // Cria documentos Swagger para v1 e v2
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DevFreela API",
                Version = "v1",
                Description = "Documentação da API - Versão 1"
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "DevFreela API",
                Version = "v2",
                Description = "Documentação da API - Versão 2"
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                    "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                    "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
}
