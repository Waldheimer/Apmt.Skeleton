namespace appointmenting.RepositoryAbstractions.Base;

public interface IRepositoryContext
{
    TRepository Set<TRepository>()
        where TRepository : class;
}
