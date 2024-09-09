using EmployeeProject.DTO;
using EmployeeProject.Models;

namespace EmployeeProject.Workers
{
	public interface IAdminWorker
	{
        public Task<EmpResponse> AddEmployeeDetails(Employee emp);
        public Task<EmpResponse> UpdateEmployeeDetails(int id, EmployeeDto emp);
        public Task<EmpResponse> DeleteEmployeeDetails(int emp);

    }
}
