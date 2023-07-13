using System.ComponentModel.DataAnnotations;

namespace WMSAPI.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
