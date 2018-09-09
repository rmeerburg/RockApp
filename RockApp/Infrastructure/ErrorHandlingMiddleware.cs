using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RockApp
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next, IHostingEnvironment env)
        { 
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (status, error) = GetStatusForException(exception);
            var result = JsonConvert.SerializeObject(CreateErrorObject(exception, error));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }

        private object CreateErrorObject(Exception exception, ApplicationErrorType type)
        {
            if(_env.IsDevelopment())
                return new { errorId = type, details = exception.ToString(), };
            return new { errorId = type };
        }

        private (HttpStatusCode Status, ApplicationErrorType Error) GetStatusForException(Exception ex)
        {
            switch (ex)
            {
                case DbUpdateException updateException:
                {
                    if (updateException?.InnerException is SqlException sqlException)
                        switch (sqlException.Number)
                        {
                            case 2627:
                                return (HttpStatusCode.BadRequest, ApplicationErrorType.DuplicateInstance);
                            case 547:
                                return (HttpStatusCode.BadRequest, ApplicationErrorType.InstanceIsReferenced);
                        }
                    break;
                }
            }
            return (HttpStatusCode.InternalServerError, ApplicationErrorType.Unknown);
        }
    }

    public enum ApplicationErrorType
    {
        Unknown,
        DuplicateInstance,
        ModelNotValid,
        InstanceIsReferenced,
    }
}