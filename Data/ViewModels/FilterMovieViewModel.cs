using JO_MOVIES.Data.Enum;
using JO_MOVIES.Models;

namespace JO_MOVIES.Data.ViewModels
{
    public class FilterMovieViewModel
    {
        public List<Movie> ?Movies { get; set; }
        public MovieType MovieType { get; set; }
        public MovieCategory MovieCategory { get; set; }
    }

}
