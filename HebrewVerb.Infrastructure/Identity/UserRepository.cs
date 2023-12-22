using HebrewVerb.Application.Identity;

namespace HebrewVerb.Infrastructure.Identity;

public class UserRepository : IdRepository<AppUser, int>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
