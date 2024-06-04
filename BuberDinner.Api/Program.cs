using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Put a scope and add services inside - seems more organized
{
    
    builder.Services.AddPresentation()
                    .AddApplication()
                    .AddIfrastructure(builder.Configuration);
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

