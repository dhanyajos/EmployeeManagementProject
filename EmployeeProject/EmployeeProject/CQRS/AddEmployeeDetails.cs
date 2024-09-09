
using EmployeeProject.DAL;
using EmployeeProject.DTO;
using EmployeeProject.Models;
using MediatR;

namespace EmployeeProject.CQRS
{
	public class AddEmployeeDetails
	{
        public record Command(Employee emp)
            : IRequest<Employee>;

        /// <summary>
        /// Defines the <see cref="Handler" />.
        /// </summary>
        public sealed class Handler : IRequestHandler<Command,Employee>
        {
            private readonly IEmployeeDetailsCommandRepository empDetailsRepository;
            
            public Handler(IEmployeeDetailsCommandRepository empDetailsRepository)
            {
               
                this.empDetailsRepository = empDetailsRepository;
            }

            public async Task<Employee> Handle(Command request,CancellationToken cancellationToken)
            {
                var result = await empDetailsRepository.SaveEmployeeDetails(request.emp);
                return result;
            }
        }
	}
}
