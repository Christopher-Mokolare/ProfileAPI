using System.ComponentModel.DataAnnotations; // Needed for data annotations

namespace ProfileAPI.Models
{
    public class UserProfile
    {
        public int Id { get; set; } // Unique identifier for each profile

        [Required] // This field is required
        public required string Name { get; set; } // Name of the user

        [Required] // This field is required
        public required  string Surname { get; set; } // Surname of the user

        [Required, EmailAddress] // This field is required and should be a valid email
        public required string Email { get; set; } // Email address of the user

        [Required] // This field is required
        public required string Phone { get; set; } // Phone number of the user

        [Required] // This field is required
        public required string FavoriteProgrammingLanguage { get; set; } // Favorite programming language
    }
}