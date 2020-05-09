using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cw4.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
                string httpQuery = httpContext.Request.QueryString.Value;

                string log = 
                $"Metoda: {httpContext.Request.Method},\r\n" +
                $"Ścieżka: {httpContext.Request.Path.Value},\r\n" +
                $"Ciało: {await new StreamReader(httpContext.Request.Body).ReadToEndAsync()},\r\n" +
                $"Query: {httpContext.Request.QueryString.Value} \r\n";
                await File.AppendAllTextAsync("requestsLog.txt", log);
            await _next(httpContext);
        }

    }
}
