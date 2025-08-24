using CoRSample.Models;

namespace CoRSample.Repositories;

public interface IEmployeeRepository
{
    public Task<Employee> GetAsync(Guid id);
    public Task<Employee> InsertAsync(Employee employee);
    public Task<Employee> UpdateAsync(Guid id, string name, int annualLeavesInHrs);
    public Task<bool> DeleteAsync(Guid id);
}