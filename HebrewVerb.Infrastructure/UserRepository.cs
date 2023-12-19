using HebrewVerb.Application;

namespace HebrewVerb.Infrastructure;

public class UserRepository : IdRepository<AppUser, int>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
