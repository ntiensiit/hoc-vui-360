using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json.Serialization;

namespace HocVui360.Api.Filters;

public class ApiResponseWrapperFilter : IAsyncResultFilter
{
    private protected record ApiResponse
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        //public string Title { get; set; }
        //public string Detail { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual dynamic? Errors { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string TraceId { get; set; }
    }

    private sealed record ApiResponse<T> : ApiResponse
    {
        public required T Data { get; set; }
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        string SuccessMessage(string message = null!)
        {
            if (!string.IsNullOrEmpty(message)) return message;

            var httpMethod = context.HttpContext.Request.Method;
            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;

            return (httpMethod.ToUpper(), controllerName, actionName) switch
            {
                ("GET", _, _) => "Data retrieved successfully",
                ("POST", _, _) => "Resource created successfully",
                ("PUT", _, _) => "Resource updated successfully",
                ("DELETE", _, _) => "Resource deleted successfully",
                _ => message ?? "Operation completed successfully"
            };
        }

        string ErrorMessage(string message = null!)
        {
            if (!string.IsNullOrEmpty(message)) return message;

            var httpMethod = context.HttpContext.Request.Method;
            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;

            return (httpMethod.ToUpper(), controllerName, actionName) switch
            {
                ("GET", _, _) => "Data retrieved failed",
                ("POST", _, _) => "Resource created failed",
                ("PUT", _, _) => "Resource updated failed",
                ("DELETE", _, _) => "Resource deleted failed",
                _ => message ?? "Operation completed failed"
            };
        }

        if (context.Result is EmptyResult || context.Result is NoContentResult)
        {
            await next();
            return;
        }

        if (context.Result is ObjectResult objectResult)
        {
            if (objectResult.Value is null || objectResult.Value is ApiResponse)
            {
                return;
            }

            var statusCode = objectResult.StatusCode;
            var success = true;
            var message = string.Empty;
            dynamic? errors = default;
            var traceId = context.HttpContext.TraceIdentifier;

            if (objectResult.Value is ValidationProblemDetails validationProblemDetails)
            {
                statusCode = validationProblemDetails.Status;
                success = false;
                message = validationProblemDetails.Title ?? validationProblemDetails.Detail ?? ErrorMessage();
                errors = validationProblemDetails.Errors.ToDictionary(kv => kv.Key, kv => kv.Value);
            }
            else if (objectResult.Value is ProblemDetails problemDetails)
            {
                statusCode = problemDetails.Status;
                success = false;
                message = problemDetails.Title ?? problemDetails.Detail ?? ErrorMessage();
            }

            object wrappedResponse;

            if (success)
            {
                wrappedResponse = new ApiResponse<object>()
                {
                    StatusCode = statusCode ?? StatusCodes.Status200OK,
                    Success = success,
                    Message = SuccessMessage(message),
                    Data = objectResult.Value,
                    TraceId = traceId,
                };
            }
            else
            {
                wrappedResponse = new ApiResponse()
                {
                    StatusCode = statusCode!.Value,
                    Success = success,
                    Message = ErrorMessage(message),
                    Errors = errors,
                    TraceId = traceId,
                };
            }

            context.Result = new OkObjectResult(wrappedResponse);
        }

        await next();
    }
}