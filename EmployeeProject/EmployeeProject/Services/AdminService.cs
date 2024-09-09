using DevExpress.DirectX.Common.Direct2D;
using EmployeeProject.DTO;
using EmployeeProject.Models;
using EmployeeProject.Workers;
using System.ComponentModel.DataAnnotations;

namespace EmployeeProject.Services
{
	public class AdminService : IAdminService
	{
        private readonly IAdminWorker fetchadminworker;
		public AdminService(IAdminWorker fetchadminworker) {
            this.fetchadminworker = fetchadminworker;
		
		}

        public async Task<EmpResponse> AddEmployeeAsync(Employee emp)
        {
            
            return await this.fetchadminworker.AddEmployeeDetails(emp);
        }

        public async Task<EmpResponse> UpdateEmployeeAsync(int id, EmployeeDto emp)
        {

            return await this.fetchadminworker.UpdateEmployeeDetails(id,emp);
        }

        public async Task<EmpResponse> DeleteEmployeeAsync(int emp)
        {

            return await this.fetchadminworker.DeleteEmployeeDetails(emp);
        }

    }
}
