using System.Net;
using System.Text.Json;
using SchoolAdmission.Domain.ResponseModels;

namespace SchoolAdmission.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred. Path: {Path}", context.Request.Path);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ApiErrorResponse
            {
                Message = "An unexpected error occurred.",
                Details = ex.Message,
                TraceId = context.TraceIdentifier
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
