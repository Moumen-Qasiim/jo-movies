using JO_MOVIES.Data.Base;
using JO_MOVIES.Models;

namespace JO_MOVIES.Data.service
{
    public class CinemasService:EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
        }
    }
}
