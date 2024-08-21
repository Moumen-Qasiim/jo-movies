using JO_MOVIES.Data.Base;
using JO_MOVIES.Data.ViewModels;
using JO_MOVIES.Models;

namespace JO_MOVIES.Data.service
{
    public interface ITicketsService : IEntityBaseRepository<Ticket>
    {
        Task<Ticket> GetMovieByIdAsync(int id);
        Task<NewTicketDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewTicket data);
        Task UpdateMovieAsync(NewTicket data);
    }
}
