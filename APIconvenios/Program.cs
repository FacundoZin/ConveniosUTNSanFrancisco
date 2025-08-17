using APIconvenios.Data;
using APIconvenios.Helpers.Validators;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Repositorio;
using APIconvenios.Services;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ConvenioQueryObjectValidator>();

builder.Services.AddScoped<IConvenioMarcoService, ConveniosMarcosServices>();
builder.Services.AddScoped<IConvenioEspecifcoService, ConvenioEspecificoService>();

builder.Services.AddScoped<_UnitOfWork>();
builder.Services.AddScoped<ConvenioQueryObjectValidator>();

builder.Services.AddScoped<IConvenioEspecificoRepository, ConvenioEspecificoRepository>();
builder.Services.AddScoped<IConvenioEspecificoReadRepository, ConvenioEspecificoReadRepository>();
builder.Services.AddScoped<IConvenioMarcoRepository, ConveniosMarcoRepository>();
builder.Services.AddScoped<IConvenioMarcoReadRepository, ConvenioMarcoReadRepository>();
builder.Services.AddScoped<ConveniosFilterService>();

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
