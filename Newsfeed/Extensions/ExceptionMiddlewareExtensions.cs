using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;

namespace Newsfeed.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = feature.Error;

                    //if (exception is DomainException)
                    //{
                    //    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    //}
                    //else
                    //{
                    //    var result = JsonConvert.SerializeObject(new { error = "Oops something went wrong" });
                    //}


                    // write to log
                    logger.Error($"Something went wrong: {exception.Message}");

                    // Returns to client
                    var result = JsonConvert.SerializeObject(new { error = exception.Message });

                    //context.Response.StatusCode = HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(result);
                });
            });
        }
    }

    //public class DomainException : Exception
    //{
    //    public DomainException(string emssa)
    //    {
    //        Message = emssa;
    //    }
    //}

    //public class CantAddArticleToSourceException : DomainException
    //{
    //    public CantAddArticleToSourceException(string message) : base(message)
    //    {
    //    }
    //}
}
