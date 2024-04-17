﻿namespace HandT_Api_Layer.DomainEntities
{
    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool? IsIndividual { get; set; }
    }
}
