using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace motcyApi.Presentation.Filters;

public class ExceptionHandlingFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var response = new ObjectResult(new
        {
            Error = "An error occurred while processing your request.",
            Message = exception.Message,
            StackTrace = exception.StackTrace
        })
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.Result = response;
        context.ExceptionHandled = true;
    }
}
