namespace ECommerence.Domain.Exceptions
{
    public class AddressNotFoundException(string username): NotFoundException($"User {username} Has No Address")
    {
        
    }
}