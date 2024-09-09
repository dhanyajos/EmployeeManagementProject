using EmployeeProject.Models;

namespace EmployeeProject.Services
{
	public interface IUserService
	{
        public Task<Employee> GetEmployeeAsync(int emp);
    }
}
