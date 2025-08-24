
using CoRSample.Repositories;

namespace CoRSample.Models;

public class Notifier : Approver
{
    public Notifier(ILogger<Approver> logger, IEmployeeRepository repository, Approver? next)
        : base(logger, repository, next)
    {
    }

    public override async Task SendRequest(LeaveRequest request)
    {
        var employee = await GetEmployee(request.EmployeeId);
        _logger.LogInformation("The request from {Name} has been approved", employee.Name);

        await Task.CompletedTask;
    }
}