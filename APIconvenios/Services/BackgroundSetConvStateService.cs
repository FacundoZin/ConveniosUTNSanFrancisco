
using APIconvenios.Interfaces.Servicios;

namespace APIconvenios.Services
{
    public class BackgroundSetConvStateService : BackgroundService
    {
        private readonly IServiceScopeFactory _ScopeFactory;
        private readonly ILogger<BackgroundSetConvStateService> _logger;
        public BackgroundSetConvStateService(IServiceScopeFactory scopeFactory, ILogger<BackgroundSetConvStateService> logger)
        {
            _ScopeFactory = scopeFactory;
            _logger = logger;

        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.Now;
                    var nextRun = now.Date.AddDays(1); 
                    var delay = nextRun - now;
                    if (delay < TimeSpan.Zero)
                    {
                        // si ya pasó la hora, ejecutamos en breve
                        delay = TimeSpan.Zero;
                    }

                    _logger.LogInformation("Esperando {Delay} hasta la próxima ejecución diaria", delay);
                    await Task.Delay(delay, stoppingToken);

                    using (var scope = _ScopeFactory.CreateScope())
                    {
                        var service = scope.ServiceProvider.GetRequiredService<IConveniosStateService>();
                        var dateForRun = DateOnly.FromDateTime(DateTime.Now);
                        _logger.LogInformation("Marcando convenios diarios para fecha {Date}", dateForRun);
                        await service.MarkConveniosAsFinished(dateForRun);
                    }
                }
                catch (TaskCanceledException)
                {
                    // la aplicación está cerrándose
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en ejecución diaria de marcación de convenios");
                }
            }
        }
    }
}
