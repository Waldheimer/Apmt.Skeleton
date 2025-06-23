using appointmenting.DbContexts;
using appointmenting.domain.Entities;
using appointmenting.Repositories.Annotations;
using appointmenting.Repositories.Base;
using appointmenting.RepositoryAbstractions;

namespace appointmenting.Repositories;

[Repository]
public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApmtDbContext context) : base(context) { }
    public Task AddRefreshTokenAsync(Guid benutzerId, string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByNameAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveRefreshTokenAsync(Guid benutzerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
