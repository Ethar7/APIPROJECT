namespace Ecommerence.Shared.ErrorModule
{
    public class ErrorToReturn
    {
        public int StatusCode {get; set;}
        public string ErrorMessage {get; set;} = null!;

        public List<string>? Errors {get; set;}
    }
}