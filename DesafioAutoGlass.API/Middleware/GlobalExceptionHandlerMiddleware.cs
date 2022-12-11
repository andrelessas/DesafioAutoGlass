using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Notifications;
using Newtonsoft.Json;

namespace DesafioAutoGlass.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public GlobalExceptionHandlerMiddleware()
        { }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ErrorReponse json;

            if (exception is ExcecoesPersonalizadas)
            {
                if (((ExcecoesPersonalizadas)exception).StatusCode > 0)
                {
                    json = new ErrorReponse(exception.Message, ((ExcecoesPersonalizadas)exception).StatusCode);
                    context.Response.StatusCode = json.StatusCode;
                }
                else
                {
                    json = new ErrorReponse(exception.Message, (int)HttpStatusCode.BadRequest);
                    context.Response.StatusCode = json.StatusCode;
                }
            }
            else
            {
                json = new ErrorReponse(exception.Message, (int)HttpStatusCode.InternalServerError);
                context.Response.StatusCode = json.StatusCode;
            }
            return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
        }
    }
}