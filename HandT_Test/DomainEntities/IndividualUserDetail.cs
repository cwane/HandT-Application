using System.ComponentModel.DataAnnotations;

namespace HandT_Api_Layer.DomainEntities
{
    public class IndividualUserDetail
    {
        public string Fullname { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Interest { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Contact_No { get; set; } = string.Empty;
        public string Citizenship_No { get; set; } = string.Empty;
        public string? Picture { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
