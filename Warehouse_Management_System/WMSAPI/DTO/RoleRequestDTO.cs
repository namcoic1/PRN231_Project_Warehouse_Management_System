using System.ComponentModel.DataAnnotations;

namespace WarehouseMSAPI.DTO
{
    public class RoleRequestDTO
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
