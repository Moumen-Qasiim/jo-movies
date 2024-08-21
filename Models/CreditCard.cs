using System.ComponentModel.DataAnnotations;
using JO_MOVIES.Data.Enum;
namespace JO_MOVIES.Models
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }

    
        public int CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public short CvcCode { get; set; }
        public string NameOfCard { get; set; } = "";


        public  required ICollection<User> ?Users { get; set; }
    }
}
