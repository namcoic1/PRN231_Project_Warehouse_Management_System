using System.ComponentModel.DataAnnotations;
using WarehouseMSAPI.DTO;

namespace WMSAPI.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int? RoleID { get; set; }
        public string? FullName { get; set; }
        public string? Title { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string? Notes { get; set; }
        public string? Picture { get; set; }
        public int? ReportsTo { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual RoleDTO? Role { get; set; }
        public virtual UserDTO? Manager { get; set; }
        public virtual LocationDTO? Location { get; set; }
    }
    public class UserRequestDTO
    {
        public int Id { get; set; }
        public int? RoleID { get; set; }
        [StringLength(50)]
        public string? FullName { get; set; }
        [StringLength(50)]
        public string? Title { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }
        [StringLength(12, MinimumLength = 10)]
        public string? ContactNumber { get; set; }
        public string? Notes { get; set; }
        public string? Picture { get; set; }
        public int? ReportsTo { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
