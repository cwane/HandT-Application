using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandT_Test_PG.DomainEntities
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Events))]
        public int EventId { get; set; }
        public virtual Events? Events { get; set; }

        public string? CategoryName { get; set; }
    }
}
