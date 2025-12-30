namespace CustomerService.Infrastructure.Persistence.UOW;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    int Commit();
}
