using EmployeeProject.Models;

namespace EmployeeProject.DAL
{
	public interface IUserDetailsQueryRepository
	{
        Task<Employee> GetEmployeeDetails(int emp);
    }
}
