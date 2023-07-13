using System.ComponentModel.DataAnnotations;

namespace WMSAPI.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int? UserID { get; set; }
        public string? CustomerID { get; set; }
        public string? SupplierID { get; set; }
        public int? LocationID { get; set; }
        public int? ProductID { get; set; }
        public int? CarrierID { get; set; }
        public int? Quantity { get; set; }
        public decimal? Freight { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual UserDTO? User { get; set; }
        public virtual CustomerDTO? Customer { get; set; }
        public virtual SupplierDTO? Supplier { get; set; }
        public virtual LocationDTO? Location { get; set; }
        public virtual ProductDTO? Product { get; set; }
        public virtual CarrierDTO? Carrier { get; set; }
        //public virtual ICollection<Report>? Reports { get; set; }
    }
    public class TransactionRequestDTO
    {
        public int Id { get; set; }
        public int? UserID { get; set; }
        public string? CustomerID { get; set; }
        public string? SupplierID { get; set; }
        public int? LocationID { get; set; }
        public int? ProductID { get; set; }
        public int? CarrierID { get; set; }
        public int? Quantity { get; set; }
        public decimal? Freight { get; set; }
        public string? Address { get; set; }
        [StringLength(12, MinimumLength = 8)]
        public string? PostalCode { get; set; }
        [StringLength(50)]
        public string? TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
