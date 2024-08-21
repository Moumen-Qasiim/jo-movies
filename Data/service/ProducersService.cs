using JO_MOVIES.Data.Base;
using JO_MOVIES.Models;

namespace JO_MOVIES.Data.service
{
    public class ProducersService: EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
        }
    }
}
