using System.ComponentModel.DataAnnotations;

namespace HandT_Api_Layer.DomainEntities
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
