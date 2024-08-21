using JO_MOVIES.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JO_MOVIES.Models
{
    public abstract class Film : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = " ";
        public string Description { get; set; } = " ";
        public string ImageURL { get; set; } = " ";
        public string Trailer { get; set; } = " ";
        public DateTime RelaseDate { get; set; } = DateTime.Now;
        public MovieCategory MovieCategory { get; set; }
        
        public double Rate { get; set; }
        public long TotalVotes { get; set; }

        //Relationships
        public List<Actor_Movie>? Actors_Movies { get; set; }


        //Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer? Producer { get; set; }

    }
}
