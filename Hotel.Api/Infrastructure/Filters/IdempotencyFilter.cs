using HotelSevice.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSevice.Api.Infrastructure.Filters
{
    public class IdempotencyFilter : IActionFilter
    {
        private readonly IIdempotencyService _idempotencyService;

        public IdempotencyFilter(IIdempotencyService idempotencyService)
        {
            _idempotencyService = idempotencyService;
        }
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string idempotencyKey = context.HttpContext.Request.Headers["Idempotency-Key"];
            if(idempotencyKey == null)
            {
                context.Result = new BadRequestObjectResult("Idempotency-Key is null");
                return;
            }
            
            var keyValue = _idempotencyService.GetKey(idempotencyKey);
            if(keyValue == null)
            {
                _idempotencyService.SetKey(idempotencyKey, "request has already been created");
                return;
            }
            context.Result = new BadRequestObjectResult(keyValue);
            return;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
