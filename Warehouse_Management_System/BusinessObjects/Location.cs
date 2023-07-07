using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Location
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("location_id")]
        public int ID { get; set; }
        [Column("user_id")]
        public int? UserID { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("capacity")]
        public int? Capacity { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("postal_code")]
        [StringLength(12, MinimumLength = 8)]
        public string? PostalCode { get; set; }
        [Column("contact_number")]
        [StringLength(12, MinimumLength = 10)]
        public string? ContactNumber { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Inventory>? Inventories { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}
