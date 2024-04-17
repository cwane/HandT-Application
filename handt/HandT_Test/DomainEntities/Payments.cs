using HandT_Test_PG.Authentication;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandT_Test_PG.DomainEntities
{
    public class Payments
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public string? Amount { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentStatus { get; set; }
        public string? EnteredBy { get; set; }
        public string? EnteredDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }


    }
}
