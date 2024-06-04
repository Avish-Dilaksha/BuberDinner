using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api;

public static class DependencyInjection
{
        public static IServiceCollection AddPresentation(this IServiceCollection services){
            services.AddControllers();

            // services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
            services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
            services.AddMAppings();
            return services;
        }
}