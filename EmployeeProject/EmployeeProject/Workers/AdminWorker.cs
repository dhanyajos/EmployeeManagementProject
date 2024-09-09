using EmployeeProject.CQRS;
using EmployeeProject.DTO;
using EmployeeProject.Models;
using MediatR;

namespace EmployeeProject.Workers
{
	public class AdminWorker:IAdminWorker
	{
        private readonly IMediator mediator;

        public AdminWorker(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<EmpResponse> AddEmployeeDetails(Employee emp)
        {
            var empres = new EmpResponse();
            var result = await this.mediator.Send(new AddEmployeeDetails.Command(emp));
            if(result != null)
            {
                empres.Result = true;
                empres.message = "Created Successfully";
            }
            else
            {
                empres.Result = false;
                empres.message = "Error Occured while processing";
            }
            
            return empres;
           
        }

        public async Task<EmpResponse> UpdateEmployeeDetails(int id, EmployeeDto emp)
        {
            var result = await this.mediator.Send(new UpdateEmployeeDetails.Command(id,emp));
            var empres = new EmpResponse();
            if (result != null)
            {
                empres.Result = true;
                empres.message = "Updated Successfully";

            }
            else
            {
                empres.Result = false;
                empres.message = "Error Occured While Updating";
            }
            return empres;

        }

        public async Task<EmpResponse> DeleteEmployeeDetails(int empid)
        {
            var result = await this.mediator.Send(new DeleteEmployeeDetails.Command(empid));
            var empres = new EmpResponse();
            if (result)
            {
                empres.Result = true;
                empres.message = "Deleted Successfully";
            }
            else
            {
                empres.Result = false;
                empres.message = "Error Occured while processing";
            }
            return empres;

        }
    }
}
