using UserService.Application.Interfaces;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Persistence.Repositories;

namespace UserService.Infrastructure.Persistence
{
public class UnitOfWork(UserDbContext dbContext) : IUnitOfWork
{
    private IUserRepository? users;

    public IUserRepository Users => users ??= new UserRepository(dbContext);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
}