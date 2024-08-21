using JO_MOVIES.Models;

namespace JO_MOVIES.Data.ViewModels
{
    public class NewTicketDropdownsVM
    {
        public NewTicketDropdownsVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }

        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
