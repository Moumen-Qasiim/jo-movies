using JO_MOVIES.Data.Base;
using JO_MOVIES.Data.ViewModels;
using JO_MOVIES.Models;

namespace JO_MOVIES.Data.service
{
    public interface IMovieService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
}
