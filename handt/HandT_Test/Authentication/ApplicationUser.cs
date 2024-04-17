using Microsoft.AspNetCore.Identity;

namespace HandT_Test_PG.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string?  isIndivCompany { get; set; }
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

        public string? Country { get; set; }
        public string? Province { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }

    }
}