using JO_MOVIES.Data.Enum;
using JO_MOVIES.Data.Static;
using System.ComponentModel.DataAnnotations;

namespace JO_MOVIES.Models
{
    public class User : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public UserRoles Roles { get; set; }
        public string profileURL { get; set; } = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.istockphoto.com%2Fphotos%2Fprofile-image&psig=AOvVaw2KV03KMN-tbvujc0r_cUN1&ust=1718714926933000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCMCVj5jW4oYDFQAAAAAdAAAAABAE";
        [Required,MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        
        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.None;

        public int ?CreditCardId { get; set; }
        public CreditCard ?CreditCard { get; set; } 
       
       // public List< OwnedTickets> ?ownedTickets { get; set; }
        public List<ShopingCartItems> ?shopingCartItems { get; set; }

        public List<Movie> Fav_Movies { get; set; }


    }
}
