using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LojaVirtual.Repository;
using LojaVirtual.Repository.Context;
using LojaVirtual.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuração do banco de dados (SQL Server)
builder.Services.AddDbContext<LojaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Configuração de CORS (Permite chamadas do frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
        policy.WithOrigins("https://localhost:7269") // 🔥 Altere para a URL do frontend
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});

// 🔹 Injeção de Dependência (Services e Repositories)
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// 🔐 Configuração do JWT
var key = Encoding.ASCII.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwibmJmIjoxNzQzNjUwMjg4LCJleHAiOjE3NDM2NTc0ODgsImlhdCI6MTc0MzY1MDI4OH0.BPsC93ifXRrlnqr2o6VQEFt-cYL-5ZupI444OJyvio0\n"); // 🔥 Troque por uma chave forte
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// 🔹 Configuração do Swagger com suporte a JWT (Adiciona o botão "Authorize" 🔓)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Loja Virtual API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT assim: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

// 🔹 Configuração dos Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 Ordem correta de Middlewares:
app.UseCors("AllowSpecificOrigin"); // 🔥 ATIVA O CORS PRIMEIRO
app.UseAuthentication();  // 🔐 ATIVA AUTENTICAÇÃO
app.UseAuthorization();   // 🔐 ATIVA AUTORIZAÇÃO

app.MapControllers();
app.Run();
