using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Authentication;

namespace HocVui360.Api.Filters;

public class ExceptionFilter(
    ILogger<ExceptionFilter> logger,
    IHostEnvironment env
) : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        logger.LogError(context.Exception, "");

        var (statusCode, message) = context.Exception switch
        {
            ArgumentException ex => (HttpStatusCode.BadRequest, ex.Message),
            KeyNotFoundException ex => (HttpStatusCode.NotFound, ex.Message),
            AuthenticationException ex => (HttpStatusCode.Unauthorized, ex.Message),
            UnauthorizedAccessException ex => (HttpStatusCode.Unauthorized, ex.Message),
            InvalidOperationException ex => (HttpStatusCode.Conflict, ex.Message),
            SqlException ex => (HttpStatusCode.ServiceUnavailable, ex.Message),
            DbUpdateException ex => (HttpStatusCode.ServiceUnavailable, ex.Message),
            TimeoutException ex => (HttpStatusCode.RequestTimeout, ex.Message),
            _ => (HttpStatusCode.InternalServerError, env.IsDevelopment() ? context.Exception.Message : ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError))
        };

        var response = new
        {
            StatusCode = (int?)statusCode,
            Success = false,
            Message = message,
            Timestamp = DateTime.Now,
            TraceId = context.HttpContext.TraceIdentifier,
        };

        context.Result = new JsonResult(response)
        {
            StatusCode = (int?)statusCode,
        };

        context.ExceptionHandled = true;

        return Task.CompletedTask;
    }
}
