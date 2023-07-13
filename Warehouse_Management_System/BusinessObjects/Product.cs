using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public int Id { get; set; }
        [Column("category_id")]
        public int? CategoryID { get; set; }
        [Column("supplier_id")]
        public string? SupplierID { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("sku")]
        [StringLength(12, MinimumLength = 8)]
        public string? SKU { get; set; }
        [Column("price")]
        public decimal? Price { get; set; }
        [Column("picture")]
        public string? Picture { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<Inventory>? Inventories { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
