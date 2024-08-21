using System.ComponentModel.DataAnnotations.Schema;

namespace JO_MOVIES.Models
{
    public class Ticket : Film
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public double Price { get; set; } = 0;

        //Cinema
        public int? CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema? Cinema { get; set; }
    }
}
