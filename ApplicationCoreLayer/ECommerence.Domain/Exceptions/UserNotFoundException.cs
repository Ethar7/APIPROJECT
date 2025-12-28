namespace ECommerence.Domain.Exceptions
{
    public sealed class UserNotFoundException(string Email): NotFoundException($"UserWithEmail{Email}IsNotFound")
    {
        
    }
}