using ArcaAPIGate.Models;
using ASWAWrapper.Common.Exceptions;
using ASWAWrapper.Common.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASWAWrapper.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    object logger = null;
                    try
                    {
                        int statusCode = context.Response.StatusCode;
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        Exception exception = context.Features.Get<IExceptionHandlerFeature>().Error;
                        logger = context.RequestServices.GetService(typeof(Logger));
                        if (exception != null && exception is HttpException)
                        {
                            statusCode = (exception as HttpException).StatusCode;
                        }
                        string action = string.Empty;
                        bool needLogging = false;
                        switch (statusCode)
                        {
                            case 400:
                                // Bad request
                                action = "400";
                                break;
                            case 401:
                                // login unsuccessful
                                action = "noaction";
                                break;
                            case 403:
                                // forbidden access
                                action = "403";
                                break;
                            case 404:
                                // page not found
                                action = "404";
                                break;
                            case 422:
                                //
                                action = "422";
                                break;
                            case 503:
                                // service unavelable
                                action = "503";
                                //needLogging = true;
                                break;
                            case 500:
                                // server error
                                action = "500";
                                needLogging = true;
                                break;

                            default:
                                action = "Index";
                                needLogging = true;
                                break;
                        }

                        try
                        {
                            if (needLogging)
                            {
                                ((Logger)logger).Log(LogTypeEnum.Error, exception: exception);

                            }
                        }
                        finally
                        {
                            context.Response.Clear();
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = statusCode;
                            await context.Response.WriteAsync(contextFeature.Error.Message);
                        }
                    }

                    catch (Exception ex)
                    {
                        if (logger != null)
                        {
                            ((Logger)logger).Log(LogTypeEnum.Error, exception: ex);
                        }
                    }
                });
            });
        }
    }
}
