using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int Id { get; set; }
        [Column("role_id")]
        public int? RoleID { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("password")]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }
        [Column("full_name")]
        [StringLength(50)]
        public string? FullName { get; set; }
        [Column("title")]
        [StringLength(50)]
        public string? Title { get; set; }
        [Column("gender")]
        public int? Gender { get; set; }
        [Column("birth_date")]
        public DateTime? BirthDate { get; set; }
        [Column("hire_date")]
        public DateTime? HireDate { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("contact_number")]
        [StringLength(12, MinimumLength = 10)]
        public string? ContactNumber { get; set; }
        [Column("notes")]
        public string? Notes { get; set; }
        [Column("picture")]
        public string? Picture { get; set; }
        [Column("reports_to")]
        public int? ReportsTo { get; set; }
        [Column("status")]
        public bool? Status { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }
        public virtual Role? Role { get; set; }
        public virtual User? Manager { get; set; }
        public virtual Location? Location { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
        public virtual ICollection<Report>? Reports { get; set; }
    }
}
