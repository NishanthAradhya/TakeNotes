using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TakeNotes.Middleware
{
    public class ErrorHandlingMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {   //each and every request will be processed
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,message: ex.ToString());
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// Global error handling for few of the exception we can add required exception types inside this Task
        /// </summary>
        /// <param name="context">Http Contecxt to handle response on exceptions</param>
        /// <param name="ex">Exception object</param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode code;
            string message;
            var exceptionType = ex.GetType();
            message = ex.Message;

            // return Error response based on the exception type
            if (exceptionType == typeof(BadHttpRequestException))
            {
                code = HttpStatusCode.BadRequest;
            }
            else if(ex.GetType() == typeof(NotImplementedException))
            {
                code = HttpStatusCode.NotImplemented;

            }
            else if (ex.GetType() == typeof(KeyNotFoundException))
            {
                code = HttpStatusCode.NoContent;
            }
            else if (ex.GetType() == typeof(NoContentResult))
            {
                code = HttpStatusCode.NoContent;
            }
            else 
            {
                code = HttpStatusCode.InternalServerError;
            }

            var result = JsonSerializer.Serialize(new { error = message , status=code });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            
            return context.Response.WriteAsync(result);
        }
    }
}
