using System.ComponentModel.DataAnnotations;
using WarehouseMSAPI.DTO;

namespace WMSAPI.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int? RoleID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
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
        public string UserName { get; set; }
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }
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
    public class UserLoginDTO
    {
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
    public class TokenDTO
    {
        public string Token { get; set; } = String.Empty;
        public string RefreshToken { get; set; } = String.Empty;
        public DateTime ValidTo { get; set; }
    }
}
