using RoboRegisService.Service;

namespace RoboRegisService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("O serviço está iniciando.");

            stoppingToken.Register(() => _logger.LogInformation("Tarefa de segundo plano está parando."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Executando tarefa: {time}", DateTimeOffset.Now);
                ServiceRobo sr = new ServiceRobo();
                await sr.ConsultarRegistros();
                stoppingToken.Register(() => _logger.LogInformation("Serviço Finalizado: {time}", DateTimeOffset.Now));
                await Task.Delay(14400000, stoppingToken);

            }

            _logger.LogInformation("O serviço está parando.");
                
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }      
}