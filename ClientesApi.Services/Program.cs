using ClientesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Registrando configurações do projeto
EntityFrameworkConfiguration.Register(builder);
CorsConfiguration.Register(builder);
SwaggerConfiguration.Register(builder);

var app = builder.Build();

//Ativando as configurações do projeto
CorsConfiguration.Use(app);
SwaggerConfiguration.Use(app);

app.UseAuthorization();

app.MapControllers();

app.Run();
