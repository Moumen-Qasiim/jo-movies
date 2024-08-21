using System.ComponentModel.DataAnnotations.Schema;

namespace JO_MOVIES.Models
{
    public class Fav_Movie
    {
        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public Movie ?movie { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        User ?user { get; set; }


    }
}
