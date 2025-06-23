using appointmenting.domain.Entities;

namespace appointmenting.RepositoryAbstractions;

public  interface IUserRepository
{
    Task<User?> GetByNameAsync(string username, CancellationToken cancellationToken = default);

    Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task AddRefreshTokenAsync(Guid benutzerId, string refreshToken, CancellationToken cancellationToken = default);

    Task RemoveRefreshTokenAsync(Guid benutzerId, CancellationToken cancellationToken = default);
}
