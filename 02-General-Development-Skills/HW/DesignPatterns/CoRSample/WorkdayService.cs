using CoRSample.Models;

namespace CoRSample;

public class WorkdayService : BackgroundService
{
    private readonly ILogger<WorkdayService> _logger;
    private readonly Approver _system;
    private readonly Employee _employee;

    public WorkdayService(ILogger<WorkdayService> logger, Approver system, Employee employee)
    {
        _logger = logger;
        _system = system;
        _employee = employee;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                Console.WriteLine("Please input a number to send a leave request: (in days)");
                var line = Console.ReadLine();
                if (!int.TryParse(line, out var days))
                {
                    throw new InvalidDataException("Input must be a integer number");
                }
                await _system.SendRequest(new LeaveRequest(_employee.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(days)));

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
