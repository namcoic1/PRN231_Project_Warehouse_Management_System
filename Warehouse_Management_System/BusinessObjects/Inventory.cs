using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Inventory
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("inventory_id")]
        public string Id { get; set; }
        [Column("location_id")]
        public int? LocationID { get; set; }
        [Column("product_id")]
        public int? ProductID { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("status")]
        public bool? Status { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        public virtual Location? Location { get; set; }
        public virtual Product? Product { get; set; }
    }
}
