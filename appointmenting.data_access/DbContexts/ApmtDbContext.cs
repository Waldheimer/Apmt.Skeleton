using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace appointmenting.DbContexts;

public class ApmtDbContext : DbContext
{
    private readonly ILogger<ApmtDbContext> _logger;

    public ApmtDbContext(DbContextOptions<ApmtDbContext> options) : base(options)
    {
        _logger = this.GetService<ILoggerFactory>().CreateLogger<ApmtDbContext>();

        ArgumentNullException.ThrowIfNull(_logger, nameof(_logger));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    public override int SaveChanges()
    {
        return SaveChangesAsync(CancellationToken.None).GetAwaiter().GetResult();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await SaveChangesAsync(CancellationToken.None);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (!ChangeTracker.HasChanges())
        {
            _logger.LogTrace("No changes were found to commit");
            return 0;
        }

        try
        {
            var savepointName = Guid.NewGuid().ToString("N").ToUpper();
            using var transaction = await Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _logger.LogTrace("Create transaction savepoint [{Savepoint}]", savepointName);
                await transaction.CreateSavepointAsync(savepointName, cancellationToken);

                _logger.LogTrace("Save changes [{Savepoint}]", savepointName);
                var result = await base.SaveChangesAsync(cancellationToken);

                _logger.LogTrace("Commit changes [{Savepoint}]", savepointName);
                await transaction.CommitAsync(cancellationToken);
                _logger.LogTrace("Commit successful [{Savepoint}]", savepointName);

                return result;
            }
            catch
            {
                _logger.LogError("Commit changes failed [{Savepoint}]", savepointName);
                _logger.LogError("Rollback transaction to savepoint [{Savepoint}]", savepointName);
                await transaction.RollbackToSavepointAsync(savepointName, cancellationToken);
                throw;
            }
        }
        catch
        {
            throw;
        }
    }
}
