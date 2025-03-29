using DevFreela.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServicesConfiguration(builder.Configuration); // Services e injeção de dependência
builder.Services.AddDatabaseConfiguration(builder.Configuration); // Banco de dados
builder.Services.AddValidationsConfiguration(); // Validações
builder.Services.AddMediatRConfiguration(); // MediatR
builder.Services.AddRoutesConfiguration(); // Rotas
builder.Services.AddVersioningConfiguration(); // Versionamento
builder.Services.AddSwaggerConfiguration(); // Documentação Swagger

var app = builder.Build();

app.AddApplicationBuilderConfigurations(); // Aplica��o

app.Run();
