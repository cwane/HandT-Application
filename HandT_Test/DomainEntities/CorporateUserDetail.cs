using System.ComponentModel.DataAnnotations;

namespace HandT_Api_Layer.DomainEntities
{
    public class CorporateUserDetail
    {
        public string Org_Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Interest { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Org_Website { get; set; } = string.Empty;
        public string Org_Reg_No { get; set; } = string.Empty;
        public string Org_Contact { get; set; } = string.Empty;
        public string Contact_Pers_Name { get; set; } = string.Empty;
        public string Contact_Pers_Email { get; set; } = string.Empty;
        public string Contact_Pers_No { get; set; } = string.Empty;
        public string Profile_Picture { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
