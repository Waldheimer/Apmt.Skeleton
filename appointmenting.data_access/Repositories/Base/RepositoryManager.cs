using appointmenting.DbContexts;
using appointmenting.RepositoryAbstractions.Base;

namespace appointmenting.Repositories.Base;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly ApmtDbContext _dbContext;

    private readonly IRepositoryContext _repositoryContext;

    public RepositoryManager(ApmtDbContext dbContext, IRepositoryContext repositoryContext)
    {
        _dbContext = dbContext;
        _repositoryContext = repositoryContext;
    }

    public IRepositoryContext Context => _repositoryContext;

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);
}