using CoRSample.Repositories;

namespace CoRSample.Models;

public abstract class Approver
{
    protected Approver? _next;
    protected readonly ILogger<Approver> _logger;
    private readonly IEmployeeRepository _repository;
    protected static Employee? _employee;

    public Approver(ILogger<Approver> logger, IEmployeeRepository repository, Approver? next)
    {
        _logger = logger;
        _repository = repository;
        _next = next;
    }

    public abstract Task SendRequest(LeaveRequest request);

    public async Task<Employee> GetEmployee(Guid id)
    {
        if (_employee == null)
        {
            var employee = await _repository.GetAsync(id);
            return _employee = employee;
        }
        return _employee;
    }
}