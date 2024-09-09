using EmployeeProject.CQRS;
using EmployeeProject.Models;
using MediatR;

namespace EmployeeProject.Workers
{
	public class UserWorker : IUserWorker
	{
        private readonly IMediator mediator;

        public UserWorker(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<Employee> GetEmployeeDetails(int empid)
        {
            var empdetails = new Employee();
            var result = await this.mediator.Send(new FetchEmployeeDetails.Query(empid));
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
