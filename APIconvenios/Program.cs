using APIconvenios.Commands.ConvenioEspecificoCommands;
using APIconvenios.Commands.ConvenioMarcoCommands;
using APIconvenios.Data;
using APIconvenios.Helpers.JsonConverters;
using APIconvenios.Helpers.Logger;
using APIconvenios.Helpers.Validators;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Middlewares;
using APIconvenios.Repositorio;
using APIconvenios.Services;
using APIconvenios.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWindowsService(Options => Options.ServiceName = "API Convenios UTN"); 

//builder.WebHost.UseUrls("http://localhost:8888");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
/*var dbPath = Path.Combine(
    appDirectory,
    "SistemaConveniosUTN",
    "SistemaConveniosUTN.db"
);
var LogPath = Path.Combine(
    appDirectory,
    "SistemaConveniosUTN",
    "Logs"
);*/

var LogPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "SistemaConveniosUTN",
    "Logs"
);

var dbPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "SistemaConveniosUTN",
    "SistemaConveniosUTN.db"
);



Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

builder.Services.AddDbContextFactory<ApplicationDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));


builder.Services.AddScoped<IConvenioMarcoService, ConveniosMarcosServices>();
builder.Services.AddScoped<IConvenioEspecifcoService, ConvenioEspecificoService>();
builder.Services.AddScoped<ConveniosFilterService>();
builder.Services.AddScoped<IConveniosDocumentManager, ConveniosDocumentsManager>();


builder.Services.AddScoped<_UnitOfWork>();

builder.Services.AddScoped<IConvenioEspecificoRepository, ConvenioEspecificoRepository>();
builder.Services.AddScoped<IConvenioEspecificoReadRepository, ConvenioEspecificoReadRepository>();
builder.Services.AddScoped<IConvenioMarcoRepository, ConveniosMarcoRepository>();
builder.Services.AddScoped<IConvenioMarcoReadRepository, ConvenioMarcoReadRepository>();
builder.Services.AddScoped<ICarreraRepository, CarrerasRepository>();
builder.Services.AddScoped<IEmpresaRepository,EmpresaRepository>();
builder.Services.AddScoped<IArchivosRepository, ArchivosRepository>();  
builder.Services.AddSingleton<ILogger>(new FileLogger(LogPath));


builder.Services.AddCors(options =>
{
    options.AddPolicy("MiPolicyElectron", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

var app = builder.Build();

// database migration
/*using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}*/

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.UseCors("MiPolicyElectron");

app.UseAuthorization();

app.MapControllers();

app.Run();
