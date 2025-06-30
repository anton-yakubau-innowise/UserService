using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Persistence.Repositories
{
    public class UserRepository(UserDbContext dbContext) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                                   .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = email.ToUpperInvariant();
            return await dbContext.Users
                                   .FirstOrDefaultAsync(v => v.Email == normalizedEmail, cancellationToken);
        }

        public async Task<IEnumerable<User>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> ListAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users
                                   .Where(predicate)
                                   .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await dbContext.Users.AddAsync(user, cancellationToken);
        }

        public void Delete(User user)
        {
            dbContext.Users.Remove(user);
        }
        public void Update(User user)
        {
            dbContext.Users.Update(user);
        }
    }
}