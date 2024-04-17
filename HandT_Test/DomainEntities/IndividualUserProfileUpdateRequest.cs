using System.ComponentModel.DataAnnotations;

namespace HandT_Api_Layer.DomainEntities
{
    public class IndividualUserProfileUpdateRequest
    {
        [Required]
        public string Fullname { get; set; } = string.Empty;
        [Required]
        public string DOB { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public string Occupation { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        [Required]
        public string Province { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Interest { get; set; } = string.Empty;
        [Required]
        public string Bio { get; set; } = string.Empty;
        [Required]
        public string Contact_No { get; set; } = string.Empty;
        [Required]
        public string Citizenship_No { get; set; } = string.Empty;
        public string? Picture { get; set; }
        
        public string Username { get; set; } = string.Empty;
        [Required]
        public bool PictureUpdated { get; set; } = false;
    }
}
