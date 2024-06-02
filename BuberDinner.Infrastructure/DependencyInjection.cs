using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Presistence;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Presistence;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
        public static IServiceCollection AddIfrastructure(
                this IServiceCollection services, 
                ConfigurationManager configuration)
        {
                services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
                
                services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
                services.AddSingleton<IDateTimeProvider, DateTimeProvider>(); 

                services.AddScoped<IUserRepository, UserRepository>();

                return services;
        }
}