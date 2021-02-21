using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using PaymentEngine.Api.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentEngine.Api.Filters
{
    public class LoggerMiddlewareFilter
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddlewareFilter> logger;
        readonly string[] _sensitiveDatakeys;

        public LoggerMiddlewareFilter(RequestDelegate next, ILogger<LoggerMiddlewareFilter> logger, IConfiguration configurationRoot)
        {
            _next = next;
            this.logger = logger;
            _sensitiveDatakeys = configurationRoot.GetValue<string>("AppSettings:SensitiveDataKeys").Split(',');
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var traceObject = CreateTraceObject(context);

                using (var requestBodyStream = new MemoryStream())
                {
                    using (var responseBodyStream = new MemoryStream())
                    {
                        #region -- process request body stream
                        var originalRequestBody = context.Request.Body;

                        await context.Request.Body.CopyToAsync(requestBodyStream);
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        traceObject.RequestMessage = new StreamReader(requestBodyStream).ReadToEnd();

                        requestBodyStream.Seek(0, SeekOrigin.Begin);
                        context.Request.Body = requestBodyStream;
                        #endregion

                        var orignalResponseBody = context.Response.Body;
                        context.Response.Body = responseBodyStream;

                        await _next(context);

                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                        traceObject.ResponseMessage = new StreamReader(responseBodyStream).ReadToEnd();
                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                        await responseBodyStream.CopyToAsync(orignalResponseBody);

                        context.Response.Body = orignalResponseBody;
                        context.Request.Body = originalRequestBody;
                    }
                }
                FinalizeAndLogTrace(traceObject, context);
            }
            catch (Exception ex)
            {

                this.logger.LogError(ex, "An error occurred");
            }
        }

        private LogViewModel CreateTraceObject(HttpContext context)
        {

            var traceObject = new LogViewModel
            {
                Uri = context.Request.Path,
                Method = context.Request.Path,
                RequestTime = DateTime.Now,
                TraceId = context.TraceIdentifier,
                ClientIp = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? context.Connection.RemoteIpAddress.ToString(),
                HostIp = context.Connection.LocalIpAddress.ToString(),
                UserAgent = context.Request.Headers[HeaderNames.UserAgent],
                RequestHeaders = GetHeaders(context.Request.Headers),
                ThreadId = context.TraceIdentifier,
                Direction = "Inbound"
            };

            return traceObject;
        }

        private void FinalizeAndLogTrace(LogViewModel traceObject, HttpContext context)
        {
            try
            {
                traceObject.ResponseTime = DateTime.Now;
                traceObject.ResponseCode = context.Response.StatusCode.ToString();
                traceObject.ResponseHeaders = GetHeaders(context.Response.Headers);
                this.logger.LogInformation("{@log}", traceObject);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred");
            }
        }
        private string GetHeaders(IHeaderDictionary headers)
        {
            if (headers == null) return "{}";

            StringBuilder sb = new StringBuilder();

            sb.Append("{\r\n");


            foreach (var header in headers)
            {

                foreach (var headerValue in header.Value)
                {
                    var value = headerValue;

                    if (_sensitiveDatakeys.Any(key => string.Equals(key, header.Key, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        value = "***Sensitive***";
                    }
                    sb.Append("  ");
                    sb.Append(header.Key);
                    sb.Append(": ");
                    sb.Append(value);
                    sb.Append("\r\n");
                }
            }

            sb.Append('}');

            return sb.ToString();
        }
    }
}
