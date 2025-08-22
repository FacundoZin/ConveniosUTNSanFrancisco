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

var dbPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "SistemaConveniosUTN",
    "SistemaConveniosUTN.db"
);

Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

builder.Services.AddDbContextFactory<ApplicationDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<ConvenioQueryObjectValidator>();

builder.Services.AddScoped<IConvenioMarcoService, ConveniosMarcosServices>();
builder.Services.AddScoped<IConvenioEspecifcoService, ConvenioEspecificoService>();
builder.Services.AddScoped<ConveniosFilterService>();
builder.Services.AddScoped<IConveniosDocumentManager, ConveniosDocumentsManager>();


builder.Services.AddScoped<_UnitOfWork>();
builder.Services.AddScoped<ConvenioQueryObjectValidator>();

builder.Services.AddScoped<IConvenioEspecificoRepository, ConvenioEspecificoRepository>();
builder.Services.AddScoped<IConvenioEspecificoReadRepository, ConvenioEspecificoReadRepository>();
builder.Services.AddScoped<IConvenioMarcoRepository, ConveniosMarcoRepository>();
builder.Services.AddScoped<IConvenioMarcoReadRepository, ConvenioMarcoReadRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173/") 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

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
