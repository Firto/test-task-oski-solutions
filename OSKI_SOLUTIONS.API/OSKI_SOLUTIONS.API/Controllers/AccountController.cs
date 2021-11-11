using System.Threading.Tasks;
using OSKI_SOLUTIONS.API.Controllers.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OSKI_SOLUTIONS.Helpers.DtoModels;
using OSKI_SOLUTIONS.Domain.Services.Abstraction;
using OSKI_SOLUTIONS.Helpers.DtoModels.Authefication;
using OSKI_SOLUTIONS.DataAccess.Entities;

namespace OSKI_SOLUTIONS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
            => _accountService = accountService;

        // security
        [UUID]
        [HttpPost]
        [MyNoAutorize]
        public async Task<ResultDto> Login([FromBody] LoginDto model, [BindNever]string uuid)
            => ResultDto.Create(await _accountService.Login(model, uuid));

        [UUID]
        [HttpPost]
        [MyNoAutorize]
        public async Task<ResultDto> RefreshToken([FromBody] RefreshTokenDto model, [BindNever]string uuid)
            => ResultDto.Create(await _accountService.RefreshToken(model, uuid));

        [UUID]
        [HttpPost]
        [MyNoAutorize]
        public async Task<ResultDto> Register([FromBody] RegisterDto model, [BindNever]string uuid)
            => ResultDto.Create(await _accountService.Register(model, uuid));

        [HttpGet]
        [MyAutorize]
        public async Task<ResultDto> Logout()
        {
            await _accountService.Logout(HttpContext.Request.Headers);
            return ResultDto.Create(null); 
        }

        [HttpGet]
        [MyAutorize]
        public async Task<ResultDto> LogoutAll()
        {
            await _accountService.LogoutAll(HttpContext.Request.Headers);
            return ResultDto.Create(null);
        }
    }
}
