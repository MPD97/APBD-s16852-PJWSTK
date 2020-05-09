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
            StreamReader sr = new StreamReader(httpContext.Request.Body);
                string log = 
                $"Metoda: {httpContext.Request.Method},\r\n" +
                $"Ścieżka: {httpContext.Request.Path.Value},\r\n" +
                $"Ciało: {await sr.ReadToEndAsync()},\r\n" +
                $"Query: {httpContext.Request.QueryString.Value} \r\n";
                await File.AppendAllTextAsync("requestsLog.txt", log);
            sr.Dispose();

            await _next(httpContext);
        }

    }
}
