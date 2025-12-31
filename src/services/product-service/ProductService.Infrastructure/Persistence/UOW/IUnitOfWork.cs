namespace ProductService.Infrastructure.Persistence.UOW;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
    int Commit();
}
