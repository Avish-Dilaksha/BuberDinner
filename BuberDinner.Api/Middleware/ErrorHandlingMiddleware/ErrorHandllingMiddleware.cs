using System.Net;
using Newtonsoft.Json;

namespace BuberDinner.Api.Middleware.ErrorHandlingMiddleware;

public class ErrorHandllingMiddleware
{
        private readonly RequestDelegate _next;

    public ErrorHandllingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        var result = JsonConvert.SerializeObject(new {error = exception.Message});
        context.Response.ContentType = "Application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}