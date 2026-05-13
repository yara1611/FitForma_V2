using Microsoft.AspNetCore.Diagnostics;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next=next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(KeyNotFoundException ex)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch(ArgumentException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            context.Response.StatusCode = 409; // Conflict
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            //log exception here later
        }
    }
}