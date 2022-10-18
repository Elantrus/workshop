using System.Net;

public class CustomExceptionMiddlerware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddlerware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IWebHostEnvironment environment)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleException(context, environment, ex);
        }
    }

    private void HandleException(HttpContext context, IWebHostEnvironment environment, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
  
        context.Response.StatusCode = (int) statusCode;

        if (environment.IsDevelopment())
            context.Response.WriteAsJsonAsync(new
            {
                Message = exception.Message,
                Ocurrence = DateTime.Now,
                StackTrace = exception.StackTrace
            });
        else
        {
            context.Response.WriteAsJsonAsync(new
            {
                Message = exception.Message,
                Ocurrence = DateTime.Now
            });
        }

    }
}