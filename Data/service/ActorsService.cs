using JO_MOVIES.Data.Base;
using JO_MOVIES.Models;

namespace JO_MOVIES.Data.service
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorService
    {
        public ActorsService(AppDbContext context) : base(context) { }
    }
}
