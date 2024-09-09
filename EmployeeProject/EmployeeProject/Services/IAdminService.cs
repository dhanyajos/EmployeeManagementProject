using EmployeeProject.DTO;
using EmployeeProject.Models;

namespace EmployeeProject.Services
{
	public interface IAdminService
	{
        public Task<EmpResponse> AddEmployeeAsync(Employee emp);
        public Task<EmpResponse> UpdateEmployeeAsync(int id, EmployeeDto emp);

        public Task<EmpResponse> DeleteEmployeeAsync(int emp);

    }
}
