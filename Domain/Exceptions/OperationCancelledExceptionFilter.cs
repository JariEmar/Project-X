using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class OperationCancelledExceptionFilter : ExceptionFilterAttribute
    {
        public OperationCancelledExceptionFilter()
        {
        }
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                //_logger.LogInformation("Request was cancelled");
                context.ExceptionHandled = true;
                context.Result = new StatusCodeResult(400);
            }
        }
    }
}
