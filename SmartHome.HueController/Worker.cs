namespace SmartHome.HueController
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HueController _hueController;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _hueController = new HueController();
            _hueController.RegisterHotKeys();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(3_600_000, stoppingToken);
            }
            if (stoppingToken.IsCancellationRequested)
                _hueController.UnregisterHotKeys();
        }
    }
}