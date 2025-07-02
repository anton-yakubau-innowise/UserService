using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using UserService.Domain.Common;

namespace UserService.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; private set; }

        [Required]
        public string LastName { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        private User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            UserName = string.Empty;
            Email = string.Empty;
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
