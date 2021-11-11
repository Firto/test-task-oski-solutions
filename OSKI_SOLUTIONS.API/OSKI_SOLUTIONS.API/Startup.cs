using System;
using System.Text;
using AutoMapper;
using OSKI_SOLUTIONS.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OSKI_SOLUTIONS.DataAccess.Entities;
using OSKI_SOLUTIONS.DataAccess.Interfaces;
using OSKI_SOLUTIONS.DataAccess.Repositories;
using OSKI_SOLUTIONS.Domain.Hostings;
using OSKI_SOLUTIONS.Domain.Services.Abstraction;
using OSKI_SOLUTIONS.Domain.Services.Configuration;
using OSKI_SOLUTIONS.Domain.Services.Implementation;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager;
using OSKI_SOLUTIONS.Helpers.Managers.CClientErrorManager.Middleware;
using OSKI_SOLUTIONS.Domain.Profiles;

namespace OSKI_SOLUTIONS.API
{

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IConfiguration _jwtConfiguration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _jwtConfiguration = _configuration.GetSection("JwtSettings");
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContext, ApplicationContext>(opt =>
                opt.UseNpgsql(_configuration.GetSection("ConnectionStrings")["mypostgres"]), optionsLifetime: ServiceLifetime.Singleton);

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper());

            // configuration

            services.AddSingleton(new TokenServiceConfiguration
            {
                TokenValidation = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = _jwtConfiguration["Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration["Key"])),
                    ClockSkew = TimeSpan.Zero
                },
                RefreshTokenValidation = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuer = _jwtConfiguration["Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration["RefreshKey"])),
                    ClockSkew = TimeSpan.Zero
                },
                TokenExpires = TimeSpan.FromMinutes(int.Parse(_jwtConfiguration["ExpireMinuts"])),
                RefreshTokenExpires = TimeSpan.FromDays(int.Parse(_jwtConfiguration["RefreshExpireDays"])),
                RefreshTokenRemove = TimeSpan.FromDays(int.Parse(_jwtConfiguration["RefreshRemoveDays"]))
            });

            // Singleton
            services.AddSingleton<ClientErrorManager>();

            // Hosted Services
            services.AddHostedService<TokenRemoveHosting<User>>();

            // Scoped Services
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(ITokenService<>), typeof(JwtTokenService<>));

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<TestsService>();
            services.AddScoped<SessionsOfTestsService>();

      services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseDeveloperExceptionPage();
            app.UseMiddleware<ClientErrorsMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
