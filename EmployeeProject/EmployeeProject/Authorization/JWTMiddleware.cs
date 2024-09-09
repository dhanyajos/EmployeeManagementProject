using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeProject.Authorization
{
	public class JWTMiddleware
	{
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="JWTMiddleware"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="LoggerFacade{JWTMiddleware}"/>.</param>
        /// <param name="next">The Request Delegate</param>
        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (string.IsNullOrEmpty(token))
                {
                    await _next(context);
                    return;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

                var roles = jwtSecurityToken.Claims.Where(c => c.Type == "role" && (c.Value == "User" || c.Value == "Admin")).Select(c => c.Value).ToList();


                if (roles.Any())
                {
                    context.Items["User"] = new TokenUserDto
                    {
                        Roles = roles
                    };
                }

                await _next(context);
            }
            catch (Exception ex)
            {
 
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }

        public class TokenUserDto
        {

            public List<string> Roles { get; set; }
        }

    }
}
