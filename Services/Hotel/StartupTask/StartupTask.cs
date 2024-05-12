

public class StartupTask : IStartupTask
{
    private readonly ILogger<StartupTask> _logger;

    public StartupTask(ILogger<StartupTask> logger)
    {
        _logger = logger;
    }

    public Task ExecuteAsync()
    {
        return new Task(() => _logger.LogDebug("Startup task executed."));
    }
}