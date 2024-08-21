using System.ComponentModel.DataAnnotations;

namespace JO_MOVIES.Data.ViewModels
{
    public class NewUserVM
    {
 

        public string profileURL { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string ConPassowrd {  get; set; } = string.Empty;
    }
}
