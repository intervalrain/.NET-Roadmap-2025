using CoRSample.Repositories;

namespace CoRSample.Models;

public class Manager : Approver
{
    public Manager(ILogger<Manager> logger, IEmployeeRepository repository, Approver? next)
        : base(logger, repository, next)
    {
    }

    public override async Task SendRequest(LeaveRequest request)
    {
        var employee = await GetEmployee(request.EmployeeId);
        var days = (request.EndDay - request.StartDay).TotalDays;

        if (employee.AnnualLeavesInHrs < days * 8)
        {
            _logger.LogInformation("Manager did not approve {Name}'s request due to the annual leaves left is not enough.", employee.Name);
        }
        else
        {
            _next?.SendRequest(request);
        }
    }
}