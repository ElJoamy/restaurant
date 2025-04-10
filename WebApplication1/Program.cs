﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Config;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración de JWT desde appsettings.json
var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = jwtConfig["Key"]!;

// ➕ Configuración CORS personalizada
builder.Services.AddCorsConfiguration();

// Agregar controladores y documentación Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApplication1", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato: Bearer {token}",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(DbConfig.GetConnectionString()));

// Servicios
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProfileService>();

builder.Services.AddScoped<CargoService>();
builder.Services.AddScoped<SucursalService>();
builder.Services.AddScoped<PersonalService>();
builder.Services.AddScoped<InventarioService>();
builder.Services.AddScoped<ComprasService>();
builder.Services.AddScoped<MesasService>();
builder.Services.AddScoped<ReservasService>();
builder.Services.AddScoped<VentasService>();
builder.Services.AddScoped<TurnoPersonalService>();
builder.Services.AddScoped<ProveedoresService>();
builder.Services.AddScoped<ProductosService>();
builder.Services.AddScoped<DetalleCompraService>();
builder.Services.AddScoped<DetallePedidoService>();
builder.Services.AddScoped<DetalleVentasService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<PedidosService>();
builder.Services.AddScoped<PagoService>();
builder.Services.AddScoped<MetodoPagoService>();
builder.Services.AddScoped<FacturasService>();
builder.Services.AddScoped<CategoriaProductosService>();

builder.Services.AddScoped<VentaFlujoService>();
builder.Services.AddScoped<FlujoCompletoService>();
builder.Services.AddScoped<FlujoCompletoCompraService>();


// Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCorsConfiguration();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
