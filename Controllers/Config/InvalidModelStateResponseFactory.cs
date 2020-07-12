using Microsoft.AspNetCore.Mvc;
using InsuranceSolution.API.Extensions;
using InsuranceSolution.API.Resources;

namespace InsuranceSolution.API.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ContactErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);
            
            return new BadRequestObjectResult(response);
        }
    }
}