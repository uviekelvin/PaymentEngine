using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using PaymentEngine.Domain.Exceptions;
using PaymentEngine.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentEngine.Api.Filters
{
    public class GlobalErrorHandler : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger<GlobalErrorHandler> _logger;


        public GlobalErrorHandler(IHostingEnvironment env, ILogger<GlobalErrorHandler> logger)
        {
            this.env = env;
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            ApiResponseDtos<string> apiResponse = new ApiResponseDtos<string>()
            {
                Code = ApiResponseCodes.ERROR,
                Payload = null
            };
            if (context.Exception.GetType() == typeof(PaymentEngineException))
            {
                apiResponse.Message = context.Exception.Message.ToString();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Result = new BadRequestObjectResult(apiResponse);
            }
            else if (context.Exception.GetType() == typeof(DbUpdateException))
            {
                var dbUpdateEx = context.Exception as DbUpdateException;
                var sqlEx = dbUpdateEx?.InnerException as SqlException;
                if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    //This is a DbUpdateException on a SQL database
                    apiResponse.Message = UniqueErrorFormatter(sqlEx, dbUpdateEx.Entries);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    context.Result = new BadRequestObjectResult(apiResponse);
                }
            }
            else
            {
                apiResponse.Message = "An error occurred please try again";
                if (env.IsDevelopment())
                {
                    apiResponse.Message = context.Exception.ToString();
                }
                context.Result = new BadRequestObjectResult(apiResponse);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var logKey = Guid.NewGuid().ToString("N");
                apiResponse.Message = $"{apiResponse.Message} Error Code: {logKey}";
                _logger.LogError(context.Exception, $"Error Code={logKey}");
            }
            context.ExceptionHandled = true;
        }


        public static string UniqueErrorFormatter(SqlException ex, IReadOnlyList<EntityEntry> entitiesNotSaved)
        {
            var message = ex.Errors[0].Message;
            var matches = UniqueConstraintRegex.Matches(message);

            if (matches.Count == 0)
                return null;
            var entityDisplayName = entitiesNotSaved.Count == 1
                ? entitiesNotSaved.Single().Entity.GetType().Name
                : matches[0].Groups[1].Value;

            var returnError = " " +
                              matches[0].Groups[2].Value + " in " +
                              entityDisplayName + ".";
            returnError = $"{entityDisplayName} with matching {matches[0].Groups[2].Value} already exists";
            return returnError;
        }

        private static readonly Regex UniqueConstraintRegex =
            new Regex("IX_([a-zA-Z0-9]*)_([a-zA-Z0-9]*)'", RegexOptions.Compiled);
    }
}
