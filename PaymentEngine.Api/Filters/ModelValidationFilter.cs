using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PaymentEngine.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentEngine.Api.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                var response = new ApiResponseDtos<IEnumerable<string>>();

                response.Code = ApiResponseCodes.INVALID_REQUEST;
                var message = context.ModelState.Values.SelectMany(a => a.Errors).Select(e => e.ErrorMessage);
                var lst = new List<string>();
                lst.AddRange(message);
                response.Message = lst.FirstOrDefault();
                response.ErrorList = lst;
                context.Result = new BadRequestObjectResult(response);
            }
        }
    }
}
