using CoRSample.Repositories;

namespace CoRSample.Models;

public class ReqValidator : Approver
{
    public ReqValidator(ILogger<ReqValidator> logger, IEmployeeRepository repository, Approver? next)
        : base(logger, repository, next)
    {
    }

    public override Task SendRequest(LeaveRequest request)
    {
        var days = (request.EndDay - request.StartDay).TotalDays;
        if (days < 0)
        {
            throw new ArgumentOutOfRangeException("Invalid input of request, end date must be later than start date");
        }
        else
        {
            _next?.SendRequest(request);
        }
        return Task.CompletedTask;
    }
}