using System.ComponentModel.DataAnnotations;

namespace WarehouseMSAPI.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
