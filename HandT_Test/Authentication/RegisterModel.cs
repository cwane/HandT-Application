using System.ComponentModel.DataAnnotations;

namespace HandT_Test.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
        public string? isIndivCompany { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Occupation { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public string? Interest { get; set; }
        public string? Bio { get; set; }
        public string? Picture { get; set; }
    }
}
