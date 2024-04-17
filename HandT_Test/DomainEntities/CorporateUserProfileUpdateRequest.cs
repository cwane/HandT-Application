using System.ComponentModel.DataAnnotations;

namespace HandT_Api_Layer.DomainEntities
{
    public class CorporateUserProfileUpdateRequest
    {
        [Required]
        public string Org_Name { get; set; } = string.Empty;
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
        public string Org_Website { get; set; } = string.Empty;
        [Required]
        public string Org_Reg_No { get; set; } = string.Empty;
        [Required]
        public string Org_Contact { get; set; } = string.Empty;
        [Required]
        public string Contact_Pers_Name { get; set; } = string.Empty;
        [Required]
        public string Contact_Pers_Email { get; set; } = string.Empty;
        [Required]
        public string Contact_Pers_No { get; set; } = string.Empty;
        [Required]
        public string Profile_Picture { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public bool PictureUpdated { get; set; } = false;
    }
}
