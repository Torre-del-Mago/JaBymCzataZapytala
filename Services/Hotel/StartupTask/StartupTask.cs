

using Hotel.Service.MessageSender;
using Messages;
using System.Diagnostics;

public class StartupTask : IStartupTask
{
    private readonly ILogger<StartupTask> _logger;

    private IMessageSender _messageSender;

    public StartupTask(ILogger<StartupTask> logger, IMessageSender messageSender)
    {
        _logger = logger;
        _messageSender = messageSender;
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine("KUCHTA2");
        await _messageSender.SendCanceledReservationEvent(new CanceledReservationCommand() { ReservationId = 1234 });
        Console.WriteLine("KUCHTA3");
    }
}