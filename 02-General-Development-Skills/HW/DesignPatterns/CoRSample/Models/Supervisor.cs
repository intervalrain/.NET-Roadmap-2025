using CoRSample.Repositories;

namespace CoRSample.Models;

public class Supervisor : Approver
{
    public Supervisor(ILogger<Supervisor> logger, IEmployeeRepository repository, Approver? next)
        : base(logger, repository, next)
    {
    }

    public override async Task SendRequest(LeaveRequest request)
    {
        var employee = await GetEmployee(request.EmployeeId);

        var days = (request.EndDay - request.StartDay).TotalDays;
        if (days > 3)
        {
            _logger.LogInformation("Supervisor did not approve {Name}'s request due to the leave request is over 3 days", employee.Name);
        }
        else
        {
            _next?.SendRequest(request);
        }
    }
}