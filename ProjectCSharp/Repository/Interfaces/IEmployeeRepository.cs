using ProjectCSharp.Models;

namespace ProjectCSharp.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>?> GetEmployees();
    }
}
