namespace ECommerence.Domain.Entities.IdentityModules
{
    public class Address
    {
        public int Id{get; set;}
        public string FirstName {get; set;} = null!;

        public string LastName {get; set;} = null!;

        public string Street{get; set;} = null!;

        public string City{get; set;}= null!;

        public string Countery{get; set;} = null!;

        public string ApplicationUserId {get; set;}

        public ApplicationUser ApplicationUser {get; set;} = null!;
    }
}