using APIconvenios.Background;
using APIconvenios.Common.Enums;
using APIconvenios.Data;
using APIconvenios.Helpers.JsonConverters;
using APIconvenios.Helpers.Logger;
using APIconvenios.Interfaces.Repositorio;
using APIconvenios.Interfaces.Servicios;
using APIconvenios.Middlewares;
using APIconvenios.Models;
using APIconvenios.Repositorio;
using APIconvenios.Services;
using APIconvenios.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Carga variables desde el archivo .env si existe (no se commitea al repositorio).
// No pisa variables ya definidas en el entorno real: tienen prioridad las del sistema.
static void CargarDotEnv()
{
    foreach (var inicio in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
    {
        var dir = Path.GetFullPath(inicio);
        for (var i = 0; i < 8 && !string.IsNullOrEmpty(dir); i++)
        {
            var candidato = Path.Combine(dir, ".env");
            if (File.Exists(candidato))
            {
                foreach (var linea in File.ReadAllLines(candidato))
                {
                    var limpia = linea.Trim();
                    if (limpia.Length == 0 || limpia.StartsWith("#")) continue;
                    var separador = limpia.IndexOf('=');
                    if (separador <= 0) continue;
                    var clave = limpia[..separador].Trim();
                    var valor = limpia[(separador + 1)..].Trim().Trim('"');
                    if (Environment.GetEnvironmentVariable(clave) is null)
                        Environment.SetEnvironmentVariable(clave, valor);
                }
                return;
            }
            dir = Path.GetDirectoryName(dir);
        }
    }
}

CargarDotEnv();

builder.Host.UseWindowsService(Options => Options.ServiceName = "API Convenios UTN");

builder.WebHost.UseUrls("http://localhost:8888");

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
    "SistemaConveniosUTNv3",
    "Logs"
);

var dbPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "SistemaConveniosUTNv3",
    "SistemaConveniosUTN.db"
);



Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

builder.Services.AddDbContextFactory<ApplicationDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<IConveniosStateService, ConveniosStateService>();
builder.Services.AddScoped<IConvenioMarcoService, ConveniosMarcosServices>();
builder.Services.AddScoped<IConvenioEspecifcoService, ConvenioEspecificoService>();
builder.Services.AddScoped<IConvenioFilterService, ConveniosFilterService>();
builder.Services.AddScoped<IConvenioGetterService, ConveniosGetterService>();
builder.Services.AddScoped<IConveniosDocumentManager, ConveniosDocumentsManager>();
builder.Services.AddScoped<IValidateConveniosService, ValidateConveniosService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


builder.Services.AddScoped<_UnitOfWork>();

builder.Services.AddScoped<IConvenioEspecificoRepository, ConvenioEspecificoRepository>();
builder.Services.AddScoped<IConvenioEspecificoReadRepository, ConvenioEspecificoReadRepository>();
builder.Services.AddScoped<IConvenioMarcoRepository, ConveniosMarcoRepository>();
builder.Services.AddScoped<IConvenioMarcoReadRepository, ConvenioMarcoReadRepository>();
builder.Services.AddScoped<ICarreraRepository, CarrerasRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IInvolucradosRepository, InvolucradosRepository>();
builder.Services.AddScoped<IArchivosRepository, ArchivosRepository>();
builder.Services.AddSingleton<ILogger>(new FileLogger(LogPath));

// Registro del hosted service
builder.Services.AddHostedService<BackgroundSetConvStateService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("MiPolicyElectron", policy =>
    {
        // Orígenes explícitos: el dev server de Vite y la app Electron (file:// => origen "null").
        // Se requiere AllowCredentials para que la cookie de sesión funcione cross-origin;
        // AllowAnyOrigin no es compatible con credenciales.
        policy.SetIsOriginAllowed(origin =>
                origin == "http://localhost:5173" ||
                origin == "http://127.0.0.1:5173" ||
                origin == "null" ||
                (origin != null && origin.StartsWith("file://", StringComparison.OrdinalIgnoreCase)))
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "convenios_auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.ExpireTimeSpan = TimeSpan.FromHours(12);
        options.SlidingExpiration = true;

        // Para una API no tiene sentido redirigir: respondemos con 401/403.
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// database migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

    // 👉 Seed del usuario admin (siempre, independiente de --seed)
    // Las credenciales SOLO se leen de variables de entorno; nunca hardcodeadas.
    var adminUsername = Environment.GetEnvironmentVariable("ADMIN_USER__NAME");
    var adminPassword = Environment.GetEnvironmentVariable("ADMIN_USER__PASSWORD");
    var adminDisplayName = Environment.GetEnvironmentVariable("ADMIN_USER__DISPLAY_NAME");

    if (string.IsNullOrWhiteSpace(adminUsername) ||
        string.IsNullOrWhiteSpace(adminPassword) ||
        string.IsNullOrWhiteSpace(adminDisplayName))
    {
        Console.WriteLine(
            "ERROR: no se pudo crear el usuario admin. " +
            "Defina las variables de entorno ADMIN_USER__NAME, ADMIN_USER__PASSWORD y ADMIN_USER__DISPLAY_NAME.");
    }
    else if (!dbContext.Usuarios.Any(u => u.Username == adminUsername))
    {
        dbContext.Usuarios.Add(new Usuario
        {
            Username = adminUsername,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword),
            Nombre = adminDisplayName,
            Rol = RolUsuario.Administrador,
            FechaCreacion = DateTime.UtcNow
        });
        dbContext.SaveChanges();
        Console.WriteLine($"Usuario admin '{adminUsername}' creado.");
    }

    // 👉 Lógica de Seeding por consola
    if (args.Contains("--seed"))
    {
        DbSeeder.Seed(dbContext);
        Console.WriteLine("Seeding finalizado. Saliendo...");
        return;
    }
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MiPolicyElectron");
app.UseMiddleware<GlobalExceptionHandler>();
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
