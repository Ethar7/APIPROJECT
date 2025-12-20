using System.Net;

namespace Ecommerence.Shared.ErrorModule
{
    public class ValidationError
    {
       public string Field { get; set; } = null!;
       public IEnumerable<String> Errors {get; set;} = [];
    }
}