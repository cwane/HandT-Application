using HandT_Test_PG.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandT_Test_PG.DomainEntities
{
    public class Registrations
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey(nameof(Events))]
        public int EventId { get; set; }
        public virtual Events? Events { get; set; }

        public string? IsIndivCompany { get; set; }
        public string? CompanyWebsite { get; set; }
        public string? NoOfPeople { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? Location { get; set; }
        public string? IsLifeInsurance { get; set; }
        public string? PaymentOption { get; set; }
        public string? EnteredBy { get; set; }
        public string? EnteredDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }
    }
}
