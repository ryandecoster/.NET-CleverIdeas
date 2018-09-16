using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleverIdeas.Models
{    
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public List<Idea> Ideas { get; set; }
        public List<Like> Likes { get; set; }
        public User()
        {
            Created_At = DateTime.Now;
            Updated_At = DateTime.Now;
            Ideas = new List<Idea>();
            Likes = new List<Like>();
        }
    }

    public class LoginUser
    {
        public string LogEmail { get; set; }

        [DataType(DataType.Password)]
        public string LogPassword { get; set; }
    }

    public class RegisterUser
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name field must not be empty.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Name must be non-numerical.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "Name must be at less than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Alias field must not be empty.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]+$", ErrorMessage = "Alias must be non-numerical.")]
        [MinLength(2, ErrorMessage = "Alias must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "Alias must be less than 50 characters.")]
        public string Alias { get; set; }

        [Required(ErrorMessage = "Email field must not be empty.")]
        [EmailAddress]
        [MaxLength(255, ErrorMessage = "Email must not exceed 255 characters.")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field must not be empty.")]
        [MinLength(8, ErrorMessage = "Password must be 8 or more characters.")]
        [MaxLength(30, ErrorMessage = "Password must not exceed 30 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm field must not be empty.")]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string Confirm { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}