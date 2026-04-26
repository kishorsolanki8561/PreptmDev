using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonService.Other
{
    public static class AddException
    {
        //public static void AddExceptionApp(WebApplication app)
        //{
        //    _ = app.UseExceptionHandler(new ExceptionHandlerOptions
        //    {
        //        ExceptionHandler = (c) =>
        //        {
        //            IExceptionHandlerFeature exception = c.Features.Get<IExceptionHandlerFeature>();
        //            HttpStatusCode statusCode = exception.Error.GetType().Name switch
        //            {
        //                "ArgumentException" => HttpStatusCode.BadRequest,
        //                _ => HttpStatusCode.ServiceUnavailable
        //            };

        //            c.Response.StatusCode = (int)statusCode;

        //            byte[] content = AppSettings.EnvironmentName.ToLower() != "dev"
        //                ? Encoding.UTF8.GetBytes($"Error [{exception.Error.Message}]")
        //                : Encoding.UTF8.GetBytes($"Error : Kindly contact system administrator.");
        //            _ = c.Response.Body.WriteAsync(content, 0, content.Length);

        //            return Task.CompletedTask;
        //        }
        //    });
        //}
    }
}
