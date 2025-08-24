using System.Collections.Concurrent;
using CoRSample.Models;

namespace CoRSample.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private static readonly ConcurrentDictionary<Guid, Employee> _dbContext = [];

    public EmployeeRepository(Employee employee)
    {
        InsertAsync(employee);   
    }

    public Task<Employee> GetAsync(Guid id)
    {
        if (!_dbContext.TryGetValue(id, out var employee))
        {
            throw new Exception("Employee not found.");
        }
        return Task.FromResult(employee);
    }

    public Task<Employee> InsertAsync(Employee employee)
    {
        if (!_dbContext.TryAdd(employee.Id, employee))
        {
            throw new Exception("Employee has existed");
        }

        return Task.FromResult(employee);
    }

    public Task<Employee> UpdateAsync(Guid id, string name, int annualLeavesInHrs)
    {
        if (!_dbContext.TryGetValue(id, out var employee))
        {
            throw new Exception("Employee not found.");
        }

        _dbContext[id] = employee;

        return Task.FromResult(employee);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return Task.FromResult(_dbContext.TryRemove(id, out _));
    }
}
