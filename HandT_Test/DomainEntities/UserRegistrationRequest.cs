using System.ComponentModel.DataAnnotations;

namespace HandT_Test_Mysql.DomainEntities
{
    public class UserRegistrationRequest
    {
        [Required]
        public string Fullname { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
