using System.Threading.Tasks;
using OSKI_SOLUTIONS.DataAccess.Entities;
using OSKI_SOLUTIONS.Helpers.DtoModels;
using Microsoft.AspNetCore.Http;
using OSKI_SOLUTIONS.Helpers.DtoModels.Authefication;

namespace OSKI_SOLUTIONS.Domain.Services.Abstraction
{
  public interface IAccountService
  {
    // security
    Task<LoggedDto> Register(RegisterDto entity, string uuid);
    Task<LoggedDto> RefreshToken(RefreshTokenDto entity, string uuid);
    Task<LoggedDto> Login(LoginDto entity, string uuid);
    Task Logout(IHeaderDictionary headers);
    Task LogoutAll(IHeaderDictionary headers);
  }
}
