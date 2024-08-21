using JO_MOVIES.Data.Enum;
using System.ComponentModel.DataAnnotations;


namespace JO_MOVIES.Data.ViewModels
{
    public class NewMovieVM 
    {
        public int Id { get; set; }

        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }



        [Display(Name = "Movie poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required")]
        public string ImageURL { get; set; } = "";


        [Display(Name = "Movie Vid URL")]
        [Required(ErrorMessage = "Movie Vid URL is required")]
        public string VidURL { get; set; } = "";



        [Display(Name = "Relase Date")]
        [Required(ErrorMessage = "Relase Date is required")]
        public DateTime RelaseDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Movie category is required")]
        public MovieCategory MovieCategory { get; set; }
        public MovieType MovieType { get; set; }

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds { get; set; }


        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerId { get; set; }

    }
}
