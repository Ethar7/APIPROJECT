namespace ECommerence.Domain.Exceptions
{
    public sealed class UnauthorizedException(string msg="Invalid UserName/Email Or Password") : Exception(msg)
    {
        
    }
}