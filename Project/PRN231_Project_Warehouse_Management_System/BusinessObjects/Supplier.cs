using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Supplier
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("supplier_id")]
        public string SupplierID { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("postal_code")]
        [StringLength(12, MinimumLength = 8)]
        public string? PostalCode { get; set; }
        [Column("contact_number")]
        [StringLength(12, MinimumLength = 10)]
        public string? ContactNumber { get; set; }
        [Column("status")]
        public bool? Status { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
