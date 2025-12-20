using System.Net;

namespace Ecommerence.Shared.ErrorModule
{
    public class ValidationErrorToReturn
    {
         public int StatusCode {get; set;} = (int)HttpStatusCode.BadRequest;

        public string Message { get; set; } = "ValidationFailed";

        public IEnumerable<ValidationError> validationErrors {get; set;}= [];
    }
}