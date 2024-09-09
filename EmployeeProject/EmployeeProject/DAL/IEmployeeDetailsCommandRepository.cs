using EmployeeProject.DTO;
using EmployeeProject.Models;

namespace EmployeeProject.DAL
{
	public interface IEmployeeDetailsCommandRepository
	{
        Task<Employee> SaveEmployeeDetails(Employee emp);
        Task<bool> UpdateEmployeeDetails(int id ,EmployeeDto emp);
        Task<bool> DeleteEmployeeDetails(int empid);
    }
}
