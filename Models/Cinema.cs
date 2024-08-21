using System.ComponentModel.DataAnnotations;

namespace JO_MOVIES.Models
{
	public class Cinema : IEntityBase
	{
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Cinema logo is required")]
        public string Logo { get; set; } = string.Empty;

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema name is required")]
        public string Name { get; set; } = "";

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema description is required")]
        public string Description { get; set; } = "";

        //Relationships
        public List<Ticket> ?Movies { get; set; }

    }
}
