using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("transaction_id")]
        public int ID { get; set; }
        [Column("user_id")]
        public int? UserID { get; set; }
        [Column("customer_id")]
        public string? CustomerID { get; set; }
        [Column("supplier_id")]
        public string? SupplierID { get; set; }
        [Column("location_id")]
        public int? LocationID { get; set; }
        [Column("product_id")]
        public int? ProductID { get; set; }
        [Column("carrier_id")]
        public int? CarrierID { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }
        [Column("freight")]
        public decimal? Freight { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("postal_code")]
        [StringLength(12, MinimumLength = 8)]
        public string? PostalCode { get; set; }
        [Column("transaction_type")]
        [StringLength(50)]
        public string? TransactionType { get; set; }
        [Column("transaction_date")]
        public DateTime? TransactionDate { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        public virtual User? User { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Location? Location { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Carrier? Carrier { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
    }
}
