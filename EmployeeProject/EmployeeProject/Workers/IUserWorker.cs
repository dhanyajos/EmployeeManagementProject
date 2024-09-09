using EmployeeProject.Models;

namespace EmployeeProject.Workers
{
	public interface IUserWorker
	{
        public Task<Employee> GetEmployeeDetails(int emp);
    }
}
