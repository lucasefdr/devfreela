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
        });
        return services;
    }
}
