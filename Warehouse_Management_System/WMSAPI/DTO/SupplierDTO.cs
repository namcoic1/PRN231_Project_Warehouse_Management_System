using System.ComponentModel.DataAnnotations;

namespace WMSAPI.DTO
{
    public class SupplierDTO
    {
        public string Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public string? Address { get; set; }
        [StringLength(12, MinimumLength = 8)]
        public string? PostalCode { get; set; }
        [StringLength(12, MinimumLength = 10)]
        public string? ContactNumber { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
