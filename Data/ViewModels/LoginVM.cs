using System.ComponentModel.DataAnnotations;


namespace JO_MOVIES.Data.ViewModels
{
    public class LoginVM
    {
        public int id { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; } =  string.Empty;   

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }    
    }
}
