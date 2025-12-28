namespace ECommerence.Domain.Exceptions
{
    public sealed class BasketNotFoundException(string Key) : NotFoundException($"Basket With Id {Key} is not Found")
    {
        
    }
}