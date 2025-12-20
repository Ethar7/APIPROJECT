using Ecommerence.Shared.ErrorModule;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerence.web.CustomMiddleWare.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            
                
                    var errors = context.ModelState.Where(M=>M.Value.Errors.Any())
                    .Select(M=>new ValidationError()
                    {
                        Field = M.Key,
                        Errors=M.Value.Errors.Select(e=>e.ErrorMessage)
                    });
                    var response = new ValidationErrorToReturn()
                    {
                        validationErrors = errors
                    };
                    return new BadRequestObjectResult(response);
            
        }
    }
}