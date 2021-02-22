using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentEngine.Infrastructure.Dtos;

namespace PaymentEngine.Api.Controllers
{

    public class BaseController : ControllerBase
    {

        protected IActionResult CustomResponse<T>(ApiResponseDtos<T> result)
        {

            switch (result.Code)
            {

                case ApiResponseCodes.INVALID_REQUEST:
                    return BadRequest(result);

                case ApiResponseCodes.NOT_FOUND:
                    return NotFound(result);

                case ApiResponseCodes.ERROR:
                    return BadRequest(result);
                case ApiResponseCodes.FAIL:
                    return BadRequest(result);
                default:
                    return Ok(result);
            }
        }

    }
}