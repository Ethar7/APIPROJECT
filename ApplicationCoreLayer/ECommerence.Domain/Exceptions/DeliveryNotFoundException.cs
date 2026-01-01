namespace ECommerence.Domain.Exceptions
{
    public class DeliveryNotFoundException(int id): 
    NotFoundException($"DeliveryMethod{id} Not Found")
    {
        
    }
}