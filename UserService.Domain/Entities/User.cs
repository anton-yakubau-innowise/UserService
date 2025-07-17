using UserService.Domain.Common;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace UserService.Domain.Entities
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<Guid>
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
    
        public User() : base()
        {
        }

        private User(string email, string userName, string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            Email = email;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static User RegisterNewUser(string email, string username, string firstName, string lastName)
        {
            Guard.AgainstInvalidEmail(email);
            Guard.AgainstNullOrWhiteSpace(username);
            Guard.AgainstNullOrWhiteSpace(firstName);
            Guard.AgainstNullOrWhiteSpace(lastName);

            return new User(email, username, firstName, lastName);
        }

        public void UpdateProfile(
            string? firstName = null,
            string? lastName = null,
            string? email = null,
            string? userName = null)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                Guard.AgainstInvalidEmail(email);
                Email = email;
            }

            if (!string.IsNullOrWhiteSpace(userName))
                UserName = userName;

            if (!string.IsNullOrWhiteSpace(firstName))
                FirstName = firstName;

            if (!string.IsNullOrWhiteSpace(lastName))
                LastName = lastName;

            SetUpdated();
        }

        private void SetUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
