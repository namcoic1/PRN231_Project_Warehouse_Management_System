using System.ComponentModel.DataAnnotations;

namespace WMSAPI.DTO
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public int? UserID { get; set; }
        public int? TransactionID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ReportType { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual UserDTO? User { get; set; }
        public virtual TransactionDTO? Transaction { get; set; }
    }
    public class ReportRequestDTO
    {
        public int Id { get; set; }
        public int? UserID { get; set; }
        public int? TransactionID { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [StringLength(50)]
        public string? ReportType { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
