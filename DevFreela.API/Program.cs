using DevFreela.API.Extensions;
using DevFreela.Infrastructure.Identity;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServicesConfiguration(builder.Configuration); // Services e injeção de dependência

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
    {
        // Configurações de senha
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        
        options.User.RequireUniqueEmail = true;
        
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
    })
    .AddEntityFrameworkStores<DevFreelaDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDatabaseConfiguration(builder.Configuration); // Banco de dados
builder.Services.AddValidationsConfiguration(); // Validações
builder.Services.AddMediatRConfiguration(); // MediatR
builder.Services.AddRoutesConfiguration(); // Rotas
builder.Services.AddVersioningConfiguration(); // Versionamento
builder.Services.AddSwaggerConfiguration(); // Documentação Swagger
builder.Services.AddJwtAuthentication(builder.Configuration); // Autenticação e autorização

var app = builder.Build();

app.AddApplicationBuilderConfigurations(); // Aplica��o

app.Run();
