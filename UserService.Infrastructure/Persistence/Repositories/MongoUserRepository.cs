using System.Linq.Expressions;
using MongoDB.Driver;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Persistence.Repositories
{
    public class MongoUserRepository(IMongoDatabase database) : IUserRepository
    {
        private readonly IMongoCollection<User> usersCollection = database.GetCollection<User>("Users");

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await usersCollection
                                   .Find(v => v.Id == id)
                                   .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = email.ToUpperInvariant();
            return await usersCollection
                                   .Find(v => v.NormalizedEmail == normalizedEmail)
                                   .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await usersCollection
                                   .Find(_ => true)
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> ListAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await usersCollection
                                   .Find(predicate)
                                   .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await usersCollection.InsertOneAsync(user, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            await usersCollection.DeleteOneAsync(v => v.Id == user.Id, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            await usersCollection.ReplaceOneAsync(v => v.Id == user.Id, user, cancellationToken: cancellationToken);
        }
    }
}