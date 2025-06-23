using appointmenting.Exceptions;
using appointmenting.RepositoryAbstractions.Base;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace appointmenting.Repositories.Base;

public class RepositoryContext : IRepositoryContext, IDisposable, IAsyncDisposable
{
    private readonly IServiceProvider _serviceProvider;

    private Dictionary<(Type Type, string? Name), object>? _repositories;

    private bool _disposed = false;

    public RepositoryContext(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TRepository Set<TRepository>()
        where TRepository : class
    {
        CheckDisposed();

        _repositories ??= new Dictionary<(Type Type, string? Name), object>();
        var type = typeof(TRepository);
        if (_repositories.TryGetValue((type, type.ShortDisplayName()), out var set))
            return (TRepository)set;

        var repository = _serviceProvider.GetService<TRepository>()
            ?? throw new RepositoryNotFoundException($"Repository not found for type {typeof(TRepository).Name}");
        _repositories[(type, type.ShortDisplayName())] = repository;

        return repository;
    }

    private void CheckDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(GetType().ShortDisplayName(), $"Cannot access a disposed '{GetType().ShortDisplayName()}' instance.");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        await Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                _repositories?.Clear();

            _disposed = true;
        }
    }
}
