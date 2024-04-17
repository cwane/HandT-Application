using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HandT_Api_Layer.DomainEntities
{
    [Table("destination_image")]
    public partial class Images
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("destinationcode")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Productcode { get; set; }

        [Column("productimage", TypeName = "blob")]
        public byte[]? Productimage { get; set; }
    }
}
