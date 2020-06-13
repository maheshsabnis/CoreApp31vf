using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp31.CustomMiddlewares
{
    /// <summary>
    /// Model class to Manage Error Schema
    /// </summary>
    public class ErrorInfo
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Class with Middleware Logic
    /// 1. Class must be ctor injected with RequestDelegate
    /// 2. Class must have public Invoke() / InvokeAsync() method to contain 
    /// logic for Middleware
    /// </summary>
    public class ErrorMiddlewareLogic
    {
        private readonly RequestDelegate request;
        /// <summary>
        /// RequestDelegate is resolved by the Host class taht is managing 
        /// all middlewares
        /// </summary>
        /// <param name="request"></param>
        public ErrorMiddlewareLogic(RequestDelegate request)
        {
            this.request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // if no exception while processing the request move to next middleware
                await request(context);
            }
            catch (Exception ex)
            {
                // logic for exception handling and generating response
                await HandleErrorAndWriteResponse(context, ex);
            }
        }

        private async Task HandleErrorAndWriteResponse(HttpContext ctx, Exception ex)
        {
            // set the error code
            ctx.Response.StatusCode = 500;
            // receive the error message from exception
            string message = ex.Message;

            // assign these values to ErrorInfo class
            var errorInfo = new ErrorInfo()
            {
                 ErrorCode = ctx.Response.StatusCode,
                 ErrorMessage = message
            };

            // write the response
            await ctx.Response.WriteAsync(JsonConvert.SerializeObject(errorInfo));
        }
    }


    // create an extension for IApplicationBuilder that will 
    // create a custom middleware method

    public static class MyExceptionMiddlewareExtension
    {
        public static void UseCustomExcpetion(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorMiddlewareLogic>();
        }
    }

}
