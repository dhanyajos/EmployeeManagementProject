using EmployeeProject.DAL;
using EmployeeProject.Models;
using MediatR;

namespace EmployeeProject.CQRS
{
	public class FetchEmployeeDetails
	{
        public record Query(int emp)
          : IRequest<Employee>;

        /// <summary>
        /// Defines the <see cref="Handler" />.
        /// </summary>
        public sealed class Handler : IRequestHandler<Query, Employee>
        {
            private readonly IUserDetailsQueryRepository userDetailsRepository;

            public Handler(IUserDetailsQueryRepository userDetailsRepository)
            {

                this.userDetailsRepository = userDetailsRepository;
            }

            public async Task<Employee> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await userDetailsRepository.GetEmployeeDetails(request.emp);
                return result;
            }
        }
    }
}
