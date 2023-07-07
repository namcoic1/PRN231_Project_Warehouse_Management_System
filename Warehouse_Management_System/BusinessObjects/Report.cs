using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Report
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("report_id")]
        public int ID { get; set; }
        [Column("user_id")]
        public int? UserID { get; set; }
        [Column("transaction_id")]
        public int? TransactionID { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("report_type")]
        [StringLength(50)]
        public string? ReportType { get; set; }
        [Column("report_date")]
        public DateTime? ReportDate { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        public virtual User? User { get; set; }
        public virtual Transaction? Transaction { get; set; }
    }
}
