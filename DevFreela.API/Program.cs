using DevFreela.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServicesConfiguration(builder.Configuration); // Services e inje��o de depend�ncia
builder.Services.AddDatabaseConfiguration(builder.Configuration); // Banco de dados
builder.Services.AddValidationsConfiguration(); // Valida��es
builder.Services.AddMediatRConfiguration(); // MediatR
builder.Services.AddRoutesConfiguration(); // Rotas
builder.Services.AddVersioningConfiguration(); // Versionamento
builder.Services.AddSwaggerConfiguration(); // Documenta��o Swagger

var app = builder.Build();

app.AddApplicationBuilderConfigurations(); // Aplica��o

app.Run();
