using EmployeeProject.Models;
using EmployeeProject.Workers;

namespace EmployeeProject.Services
{
	public class UserService : IUserService
	{
        private readonly IUserWorker fetchuserworker;
        public UserService(IUserWorker fetchuserworker)
        {
            this.fetchuserworker = fetchuserworker;

        }

       
        public async Task<Employee> GetEmployeeAsync(int emp)
        {

            return await this.fetchuserworker.GetEmployeeDetails(emp);
        }
    }
}
