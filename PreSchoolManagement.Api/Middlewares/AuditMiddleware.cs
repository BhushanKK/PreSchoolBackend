using SchoolManagement.Domain;
using PreSchoolManagement.Api.Extensions;

namespace PreSchoolManagement.Api.Middlewares;
public class AuditMiddleware(RequestDelegate next, AuditContext auditContext)
{
    public async Task InvokeAsync(HttpContext context)
    {
        auditContext.Set(new AuditContextItem
        {
            UserId = context.User.GetUserId(),
            UserName = context.User.GetUserName(),
            RequestMethod = context.Request.Method,
            RequestPath = context.Request.Path.ToString()
        });

        await next(context);
        auditContext.Clear();
    }
}
