using JO_MOVIES.Data.Base;
using JO_MOVIES.Models;

namespace JO_MOVIES.Data.service
{
    public class UserService : EntityBaseRepository<User>, IUserService
    {
        public UserService(AppDbContext context) : base(context)
        {
        }
    }
}
