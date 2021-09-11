using BaseProject.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception e)
            {

                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
          
            
            httpContext.Response.ContentType = "application/json";
            string message = "";
            Type[] whiteArrayOfException = new Type[] {typeof(ValidationException),typeof(AuthorizationException) };

            ErrorDetail errorDetail = new ErrorDetail();

            if (whiteArrayOfException.Contains(e.GetType()))
            {
                
                if (e.GetType() == typeof(ValidationException))
                    {
                        errorDetail.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        errorDetail.Errors = ((ValidationException)e).Errors.Select(e=>e.ErrorMessage).ToArray();
                    }
                else if (e.GetType() == typeof(AuthorizationException))
                        errorDetail.StatusCode = (int)HttpStatusCode.Unauthorized;

                httpContext.Response.StatusCode = errorDetail.StatusCode;
                errorDetail.Message = e.Message;
            }
            else
            {
                httpContext.Response.StatusCode=errorDetail.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorDetail.Message = "Internal Server Error";
            }

            return httpContext.Response.WriteAsync(errorDetail.ToString());
            
        }
    }
}
