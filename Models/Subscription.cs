using JO_MOVIES.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace JO_MOVIES.Models
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        public SubscriptionType Type { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public DateTime Created { get; set; }= DateTime.Now;

        public double price { get; set; }
    }
}
