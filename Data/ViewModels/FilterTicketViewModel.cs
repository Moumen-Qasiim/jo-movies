using JO_MOVIES.Data.Enum;
using JO_MOVIES.Models;

namespace JO_MOVIES.Data.ViewModels
{
    public class FilterTicketViewModel
    {
        public List<Ticket>? Movies { get; set; }
        public MovieCategory MovieCategory { get; set; }
    }
}
