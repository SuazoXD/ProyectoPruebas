using API.Configuration;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Cargar la configuración de la base de datos
var dbConfig = new DBConnections();

// Registrar el DbContext utilizando SQL Server
builder.Services.AddDbContext<ProjectDBContext>(options =>
    options.UseSqlServer(dbConfig.ConnectionString));

// Registrar dependencias: repositorios y servicios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Configurar servicios de controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// Mapear controladores
app.MapControllers();

app.Run();
