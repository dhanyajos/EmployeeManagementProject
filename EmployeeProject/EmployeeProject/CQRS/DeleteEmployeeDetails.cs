using EmployeeProject.DAL;
using EmployeeProject.Models;
using MediatR;

namespace EmployeeProject.CQRS
{
	public class DeleteEmployeeDetails
	{
        public record Command(int empid)
           : IRequest<bool>;

        /// <summary>
        /// Defines the <see cref="Handler" />.
        /// </summary>
        public sealed class Handler : IRequestHandler<Command, bool>
        {
            private readonly IEmployeeDetailsCommandRepository empDetailsRepository;

            public Handler(IEmployeeDetailsCommandRepository empDetailsRepository)
            {

                this.empDetailsRepository = empDetailsRepository;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                bool result = await empDetailsRepository.DeleteEmployeeDetails(request.empid);
                return result;
            }
        }
    }
}
