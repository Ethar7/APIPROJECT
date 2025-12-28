namespace ECommerence.Domain.Exceptions
{
    public sealed class BadRequestException(List<string> errors):Exception("Validation Failes")
    {
        public List<string> Errors {get; set;} = errors;
    }
}