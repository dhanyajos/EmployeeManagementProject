using EmployeeProject.DAL;
using EmployeeProject.DTO;
using EmployeeProject.Models;
using MediatR;

namespace EmployeeProject.CQRS
{
    public class UpdateEmployeeDetails
    {
        public record Command(int id, EmployeeDto emp)
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
                var result = await empDetailsRepository.UpdateEmployeeDetails(request.id,request.emp);
                return result;
            }
        }
    }
}
