using System.ComponentModel.DataAnnotations;

namespace WMSAPI.DTO
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public int? UserID { get; set; }
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? ContactNumber { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual UserDTO? User { get; set; }
    }
    public class LocationRequestDTO
    {
        public int Id { get; set; }
        public int? UserID { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public string? Address { get; set; }
        [StringLength(12, MinimumLength = 8)]
        public string? PostalCode { get; set; }
        [StringLength(12, MinimumLength = 10)]
        public string? ContactNumber { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
