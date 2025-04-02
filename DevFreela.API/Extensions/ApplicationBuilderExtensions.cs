using Asp.Versioning.ApiExplorer;

namespace DevFreela.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void AddApplicationBuilderConfigurations(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // Obtém o IApiVersionDescriptionProvider do provedor de serviços final
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }

        app.UseHttpsRedirection();

        // Configurações de autorização e autenticação
        app.UseAuthorization();
        app.UseAuthorization();

        app.MapControllers();
    }
}
