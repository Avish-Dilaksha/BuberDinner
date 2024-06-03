using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware.ErrorHandlingMiddleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Put a scope and add services inside - seems more organized
{
    builder.Services.AddControllers();
    builder.Services.AddApplication()
                    .AddIfrastructure(builder.Configuration);

    // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
//Put a scope and add pipline inside
{
    //app.UseMiddleware<ErrorHandllingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHsts();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseAuthentication();
    app.Run();
}

