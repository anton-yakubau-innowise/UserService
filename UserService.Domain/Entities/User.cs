using UserService.Domain.Common;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace UserService.Domain.Entities
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<Guid>
    {

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }
    
        public User() : base()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            UserName = string.Empty;
            Email = string.Empty;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
        }
        public static User RegisterNewUser(string email, string username, string firstName, string lastName)
        {
            Guard.AgainstInvalidEmail(email);
            Guard.AgainstNullOrWhiteSpace(username);
            Guard.AgainstNullOrWhiteSpace(firstName);
            Guard.AgainstNullOrWhiteSpace(lastName);

            return new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };
        }

        public void UpdateProfile(string? firstName, string? lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                FirstName = firstName;
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                LastName = lastName;
            }

            SetUpdated();
        }

        private void SetUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
