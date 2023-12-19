using HebrewVerb.Application;

namespace HebrewVerb.Infrastructure;

public class RoleRepository : IdRepository<AppRole, int>, IRoleRepository
{
    public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
