using System.Net;
using System.Text.Json;
using MusicLibraryAPI.Exceptions;

namespace MusicLibraryAPI.Midllewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        
        catch (BaseException ex)
        {
            context.Response.StatusCode = (int)ex.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerialize(ex));
        }
        
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message
            }));
        }
    }

    private static string JsonSerialize(BaseException exception)
    {
        return JsonSerializer.Serialize(
            new
            {
                StatusCodes = exception.StatusCode,
                Message = exception.Message
            }
        );
    }
}