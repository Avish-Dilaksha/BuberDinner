using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware.ErrorHandlingMiddleware;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Put a scope and add services inside - seems more organized
{
    builder.Services.AddControllers();
    builder.Services.AddApplication()
                    .AddIfrastructure(builder.Configuration);

    // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
}

var app = builder.Build();

// Configure the HTTP request pipeline.
//Put a scope and add pipline inside
{
    //app.UseMiddleware<ErrorHandllingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseAuthentication();
    app.Run();
}

