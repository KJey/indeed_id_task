using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace indeed_id.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbException ex)
            {
                await HandleDbExceptionAsync(context, ex);
            }
            catch (NotImplementedException ex)
            {
                await HandleNotImplementedExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleNotImplementedExceptionAsync(HttpContext context, NotImplementedException exception)
        {
            await WriteExceptionAsync(context, HttpStatusCode.NotImplemented,
                "Функционал не доступен", exception.Message).ConfigureAwait(false);
        }

        private async Task HandleDbExceptionAsync(HttpContext context, DbException exception)
        {
            await WriteExceptionAsync(context, HttpStatusCode.InsufficientStorage,
                "Ошибка в чтении/записи данных", exception.Message).ConfigureAwait(false);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            await WriteExceptionAsync(context, HttpStatusCode.InternalServerError,
                exception.GetType().Name, exception.Message).ConfigureAwait(false);
        }

        private async Task WriteExceptionAsync(HttpContext context, HttpStatusCode code, string type, params string[] messages)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                messages,
                type,
            })).ConfigureAwait(false);
        }
    }
}
