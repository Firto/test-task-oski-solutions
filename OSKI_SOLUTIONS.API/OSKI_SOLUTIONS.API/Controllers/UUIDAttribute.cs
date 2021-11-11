using Microsoft.AspNetCore.Mvc.Filters;
using OSKI_SOLUTIONS.Helpers.Managers;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager.Middleware;

namespace OSKI_SOLUTIONS.API.Controllers.Attributes
{
  public class UUIDAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      string uuid;

      if (!UUIDHandler.CheckUUID(context.HttpContext.Request.Headers, out uuid))
        throw new ClientException("no-uuid");
      else
        context.ActionArguments["uuid"] = uuid;
    }
  }
}
