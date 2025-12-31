using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProductService.Infrastructure.Persistence.UOW;

public class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{

    private readonly TContext dbContext;
    private readonly ILogger<TContext> logger;

    public UnitOfWork(TContext dbContext, ILogger<TContext> logger)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.logger = logger;
    }

    public int Commit()
    {
        try
        {
            return dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while committing UnitOfWork");
            throw;
        }
    }


    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while committing UnitOfWork");
            throw;
        }
    }
}
