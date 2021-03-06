using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager.Middleware
{
    public class ClientErrorsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ClientErrorManager _clientErrorManager;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ClientErrorsMiddleware(RequestDelegate next, ClientErrorManager clientErrorManager)
        {
            _next = next;
            _clientErrorManager = clientErrorManager;
            _jsonSerializerSettings = new JsonSerializerSettings();
            _jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ClientException ex)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(_clientErrorManager.MapClientErrorDtoToResultDto(ex.Id, ex.AdditionalInfo), _jsonSerializerSettings));
            }
        }
    }
}