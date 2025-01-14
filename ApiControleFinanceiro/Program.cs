using ApiControleFinanceiro.Context;
using ApiControleFinanceiro.Mappings;
using ApiControleFinanceiro.Repositories;
using ApiControleFinanceiro.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
         options.UseSqlServer(connection,
             // especifica o nome do assembly onde as migrações estão localizadas
             b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));


builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IInstituicaoFinanceiraRepository, InstituicaoFinanceiraRepository>();
builder.Services.AddScoped<IMeioDePagamentoRepository, MeioDePagamentoRepository>();
builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
builder.Services.AddScoped<ITipoDeContaRepository, TipoDeContaRepository>();
builder.Services.AddScoped<ITipoMovimentacaoRepository, TipoMovimentacaoRepository>();

builder.Services.AddAutoMapper(typeof(DomainToDTOProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
