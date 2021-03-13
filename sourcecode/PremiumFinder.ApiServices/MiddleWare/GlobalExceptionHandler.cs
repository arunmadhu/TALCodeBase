using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace PremiumFinder.ApiServices.MiddleWare
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly bool _isDevelopment;

        /// <summary>
        /// GlobalExceptionHandler 
        /// </summary>
        /// <param name="next">next- Reference for the next Middleware in the pipeline</param>
        /// <param name="logger"> reference fo the current logprovider</param>
        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, bool isDevelopment)
        {

            _next = next;
            _isDevelopment = isDevelopment;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                //user need to be logged out of the system in case of 401 error.
                await HandleException(401, ex, context);

            }
            catch (Exception ex) //For now treat all other exception as general exception. we might sort that out later on!
            {
                //user need to be redirected to be shown the error page or general error page in case of 500 error. 
                await HandleException(500, ex, context);
            }
        }


        private async Task HandleException(int statusCode, Exception ex, HttpContext context)
        {
            string messageToLogged = "";
            string messageToReport = "";

            //We need to log the complete message in DB
            messageToLogged = ex.Message + "-" + ex.StackTrace;


            if (_isDevelopment)
            {
                //if Development then report complete message..
                messageToReport = messageToLogged;
            }
            else
            {
                //In case of production we would show generic error message.
                messageToReport = "";
            }
            _logger.LogError(messageToLogged);
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(
                 new { ErrorMessage = messageToReport, IsDevelopment = _isDevelopment }
            ));
        }
    }
}
