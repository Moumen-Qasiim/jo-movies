using JO_MOVIES.Data.Base;
using JO_MOVIES.Data.Enum;
using JO_MOVIES.Data.ViewModels;
using JO_MOVIES.Models;
using Microsoft.EntityFrameworkCore;

namespace JO_MOVIES.Data.service
{
    public class MoviesService : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context) {
        
            _context = context;
        }


        public Task<List<Movie>> GetAllAsync()
        {
            return _context.Films.OfType<Movie>().Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor).ToListAsync();
        }
        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {

                Name = data.Name,
                Description = data.Description,
                 VidURL = data.VidURL,
                ImageURL = data.ImageURL,
                RelaseDate = data.RelaseDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId,
               
                 
            };
            await _context.Films.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
            }

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            var movieDetails = await  _context.Films.OfType<Movie>().Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
               
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Films.OfType<Movie>().FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
               
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> getAllByCatogry(MovieCategory movieCategory)
        {
            return await _context.Films.OfType<Movie>().Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor).Where(o => o.MovieCategory == movieCategory).ToListAsync ();
        }
    }
}
