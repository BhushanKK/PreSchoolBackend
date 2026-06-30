using SchoolManagement.Domain;

namespace SchoolAdmission.Api.Middlewares;

public class AuditMiddleware(RequestDelegate next, AuditContext auditContext)
{

    public async Task InvokeAsync(HttpContext context)
    {
        auditContext.Set(new AuditContextItem
        {
            UserId = context.User?.Identity?.Name ?? "system",
            UserName = context.User?.Identity?.Name ?? "system",
            RequestMethod = context.Request.Method,
            RequestPath = context.Request.Path.ToString()
        });

        await next(context);

        auditContext.Clear();
    }
}
