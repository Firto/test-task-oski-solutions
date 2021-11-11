using AutoMapper;
using OSKI_SOLUTIONS.DataAccess.Entities;
using OSKI_SOLUTIONS.DataAccess.Interfaces;
using OSKI_SOLUTIONS.Domain.Services.Abstraction;
using OSKI_SOLUTIONS.Helpers.Managers;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager.Middleware;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OSKI_SOLUTIONS.Helpers.DtoModels.Authefication;

namespace OSKI_SOLUTIONS.Domain.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _usersGR;
        private readonly ITokenService<User> _tokenService;
        private readonly IMapper _mapper;

        public AccountService(  ClientErrorManager clientErrorManager,
                                IGenericRepository<User> usersGR,
                                ITokenService<User> tokenService,
                                IMapper mapper)
        {
            _usersGR = usersGR;
            _tokenService = tokenService;
            _mapper = mapper;

            if (!clientErrorManager.IsIssetErrors("Account"))
                clientErrorManager.AddErrors(new ClientErrors("Account",
                    new Dictionary<string, ClientError>
                    {
                        {"no-login", new ClientError("You are not logged in!")},
                        {"already-login", new ClientError("You are already logged in!")},
                        {"base-account-err", new ClientError("Error in account service!")}
                    },
                    new List<ClientErrors>(){
                        new ClientErrors("Login", new Dictionary<string, ClientError>()
                        {
                            {"input-login", new ClientError("Please input login!")},
                            {"input-password", new ClientError("You are already logged in!")},
                            {"inc-log-pass", new ClientError("Incorrect login or password!")},
                            {"too-many-devices", new ClientError("Too many devices logged!")}
                        }),
                        new ClientErrors("Register", new Dictionary<string, ClientError>()
                        {
                            {"r-input-login", new ClientError("Input your login!")},
                            {"too-long-login", new ClientError("Too long login(max 25 characters)!")},
                            {"r-input-pass", new ClientError("Input your password!")},
                            {"login-spec-chars", new ClientError("Login musn't have specials chars!")},
                            {"pass-count-chars", new ClientError("Password must have eight and more chars!")},
                            {"pass-count-digit", new ClientError("Password must have minimum one digit!")},
                            {"too-long-pass", new ClientError("Too long password(max 25 characters)!")},
                            {"input-repeat-pass", new ClientError("Input repeat your password!")},
                            {"inc-repeat-pass", new ClientError("Passwords do not match!")},
                            {"already-registered", new ClientError("User with this login is already registered!")}
                        }),
                        new ClientErrors("Edit", new Dictionary<string, ClientError>(){
                            {"inc-email", new ClientError("Incorrect email!")},
                            {"alrd-reg-login", new ClientError("User with this login is already registered!")},
                            {"alrd-reg-email", new ClientError("User with this email is already registered!")},
                            {"inc-profile-image", new ClientError("Incorrect profile image!")}
                        })
                    }
                ));
        }

        void ValidateLogin(string login) {
            if (string.IsNullOrEmpty(login))
                throw new ClientException("r-input-login");
            else if (login.Length > 25)
                throw new ClientException("too-long-login");
            else if (!Regex.Match(login, "^[a-zA-Z_0-9]*$").Success)
                throw new ClientException("login-spec-chars");
        }

        void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ClientException("r-input-pass");
            else if (password.Length > 25)
                throw new ClientException("too-long-pass");
            else if (password.Length < 8)
                throw new ClientException("pass-count-chars");
            else if (!password.Any(c => char.IsDigit(c)))
                throw new ClientException("pass-count-digit");
        }

        public async Task<LoggedDto> Login(LoginDto entity, string uuid)
        {
            if (string.IsNullOrEmpty(entity.Login))
                throw new ClientException("input-login");
            else if (string.IsNullOrEmpty(entity.Password))
                throw new ClientException("input-password");

            var users = await _usersGR.GetAllAsync((x) => x.Login == entity.Login);
            if (users.Count() != 1 || !PasswordHandler.Validate(entity.Password, users.First().PasswordHash))
                throw new ClientException("inc-log-pass");

            if (_tokenService.CountLoggedDevices(users.First()) > 10)
                throw new ClientException("too-many-devices");

            return await _tokenService.Login(users.First(), uuid);
        }

        public async Task<LoggedDto> Register(RegisterDto entity, string uuid)
        {
            ValidateLogin(entity.Login);
            ValidatePassword(entity.Password);

            if (string.IsNullOrEmpty(entity.ConfirmPassword))
                throw new ClientException("input-repeat-pass");
            else if (entity.ConfirmPassword != entity.Password)
                throw new ClientException("inc-repeat-pass");

            var user = new User
            {
                Login = entity.Login,
                PasswordHash = PasswordHandler.CreatePasswordHash(entity.Password)
            };

            if (_usersGR.CountWhere((x) => x.Login == entity.Login) > 0)
                throw new ClientException("already-registered");

            await _usersGR.CreateAsync(user);
            return await Login(new LoginDto { Login = entity.Login, Password = entity.Password }, uuid);
        }

        public async Task<LoggedDto> RefreshToken(RefreshTokenDto entity, string uuid)
            => await _tokenService.RefreshToken(entity.RefreshToken, uuid);

        public async Task Logout(IHeaderDictionary headers)
            => await _tokenService.DeactivateToken(headers);

        public async Task LogoutAll(IHeaderDictionary headers)
            => await _tokenService.DeactivateAllTokens(headers);
    }
}
