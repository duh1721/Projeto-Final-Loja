using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Repositorio;
using ProjetoLoja.Repositorio.Interfaces;
using ProjetoLoja.Aplicacao;
using ProjetoLoja.Aplicacao.Interfaces;


var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<ProjetoLojaContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
              .AllowAnyHeader()
              .AllowAnyMethod();
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

app.MapControllers();

app.Run();

