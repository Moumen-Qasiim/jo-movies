using System.ComponentModel.DataAnnotations;

namespace JO_MOVIES.Models
{
    public class ShopingCartItems
    {
        [Key]
        public int CartItemId { get; set; }
        public required string Email { get; set; }
        public int MovieId { get; set; }
        
        public int UserId { get; set; }

        public bool Onwed  { get; set; } = false;



    }
}
