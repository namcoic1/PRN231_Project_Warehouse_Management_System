using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("category_id")]
        public int CategoryID { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("status")]
        public bool? Status { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
