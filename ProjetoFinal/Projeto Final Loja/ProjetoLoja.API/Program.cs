using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Repositorio;
using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Aplicacao;
using ProjetoLoja.Aplicacao.Interfaces;
using System.Text.Json.Serialization;
using ProjetoLoja.Servicos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"];

// Repositorios
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
builder.Services.AddScoped<IItensPedidoRepositorio, ItensPedidoRepositorio>();
builder.Services.AddScoped<ITipoProdutoRepositorio, TipoProdutoRepositorio>();

// Aplicacao
builder.Services.AddScoped<IProdutoAplicacao, ProdutoAplicacao>();
builder.Services.AddScoped<IClienteAplicacao, ClienteAplicacao>();
builder.Services.AddScoped<IEnderecoAplicacao, EnderecoAplicacao>();
builder.Services.AddScoped<IPedidoAplicacao, PedidoAplicacao>();
builder.Services.AddScoped<IItensPedidoAplicacao, ItensPedidoAplicacao>();
builder.Services.AddScoped<ITipoProdutoAplicacao, TipoProdutoAplicacao>();


builder.Services.AddScoped<ILoginCliente, LoginAplicacao>();

builder.Services.AddHttpClient<IIAService, IAService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<ProjetoLojaContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer SEU_TOKEN"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

