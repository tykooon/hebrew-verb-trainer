using HebrewVerb.Application.Identity;

namespace HebrewVerb.Infrastructure.Identity;

public class RoleRepository : IdRepository<AppRole, int>, IRoleRepository
{
    public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
