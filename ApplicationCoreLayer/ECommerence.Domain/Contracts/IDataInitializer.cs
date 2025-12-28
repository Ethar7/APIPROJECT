namespace ECommerence.Domain.Contracts
{
    public interface IDataInitializer
    {
        Task InitilizeAsync();
        Task IdentityDataSeedAsync();
    }
}