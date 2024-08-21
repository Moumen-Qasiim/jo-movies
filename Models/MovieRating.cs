using System.ComponentModel.DataAnnotations;

namespace JO_MOVIES.Models
{
    public class MovieRating
    {
        [Key]
        public int Id { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Range(1, 5)] // Example range for rating, adjust as needed
        public int Rating { get; set; } // Rating scale typically from 1 to 5
    }
}
