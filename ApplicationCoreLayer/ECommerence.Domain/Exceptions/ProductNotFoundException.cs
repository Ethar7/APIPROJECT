namespace ECommerence.Domain.Exceptions
{
    public sealed class ProductNotFoundException(int id) :  NotFoundException($"Product With Id : {id} is Not Found")
    {
        
    }
}