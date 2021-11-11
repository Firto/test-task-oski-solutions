using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using OSKI_SOLUTIONS.DataAccess.Entities;
using OSKI_SOLUTIONS.Domain.Services.Abstraction;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager.Middleware;

namespace OSKI_SOLUTIONS.API.Controllers.Attributes
{
    public class MyNoAutorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                context.HttpContext.RequestServices.GetService<ITokenService<User>>().GetUser(context.HttpContext);
                throw new ClientException("already-login");
            }
            catch (ClientException)
            {}   
        }
    }
}
