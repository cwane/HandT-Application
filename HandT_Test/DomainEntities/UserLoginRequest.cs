using System.ComponentModel.DataAnnotations;

namespace HandT_Api_Layer.DomainEntities
{
    public class UserLoginRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
