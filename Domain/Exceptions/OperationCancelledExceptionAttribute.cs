using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;

namespace Domain.Exceptions
{
    public class OperationCancelledExceptionAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public OperationCancelledExceptionAttribute(ILogger logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                logger.Information("Operation cancelled!");
                context.ExceptionHandled = true;
                context.Result = new StatusCodeResult(400);
            }
        }
    }
}
