using JO_MOVIES.Data.Base;
using JO_MOVIES.Data.Enum;
using JO_MOVIES.Data.ViewModels;
using JO_MOVIES.Models;
using Microsoft.EntityFrameworkCore;

namespace JO_MOVIES.Data.service
{
    public class TicketsService : EntityBaseRepository<Ticket>, ITicketsService
    {
        private readonly AppDbContext _context;
        public TicketsService(AppDbContext context)  : base(context) 
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Films.OfType<Ticket>()
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor).ToListAsync();
        }

        public async Task AddNewMovieAsync(NewTicket data)
        {
            var newMovie = new Ticket()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId,
                
            };
            await _context.Films.AddAsync(newMovie);

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

        public async Task<Ticket?> GetMovieByIdAsync(int id)
        {
            Ticket? movieDetails = await _context.Films.OfType<Ticket>()
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewTicketDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewTicketDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewTicket data)
        {
            var dbMovie = await _context.Films.OfType<Ticket>().FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
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

        public async Task<List<Ticket>> getAllByCategory(MovieCategory movieCategory)
        {
            return await _context.Films.OfType<Ticket>()
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor).Where(o=> o.MovieCategory == movieCategory).ToListAsync();
        }
    }
}

