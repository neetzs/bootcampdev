using GestionProductos.Api.Repositories;
using GestionProductos.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios de controladores
builder.Services.AddControllers();

// 2. Configurar Swagger/OpenAPI para documentar y probar los endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Inyección de Dependencias: registro de Repository y Service con AddScoped
builder.Services.AddScoped<IProductoRepository, ProductoRepositoryInMemory>();
builder.Services.AddScoped<IProductoService, ProductoService>();

var app = builder.Build();

// Configurar el pipeline de HTTP requests
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();