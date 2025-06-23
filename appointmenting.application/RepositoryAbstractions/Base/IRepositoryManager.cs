namespace appointmenting.RepositoryAbstractions.Base;

public interface IRepositoryManager
{
    IRepositoryContext Context { get; }

    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}