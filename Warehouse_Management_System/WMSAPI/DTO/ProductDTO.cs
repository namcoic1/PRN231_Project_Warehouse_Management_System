using System.ComponentModel.DataAnnotations;

namespace WMSAPI.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int? CategoryID { get; set; }
        public string? SupplierID { get; set; }
        public string? Name { get; set; }
        public string? SKU { get; set; }
        public decimal? Price { get; set; }
        public string? Picture { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual CategoryDTO? Category { get; set; }
        public virtual SupplierDTO? Supplier { get; set; }
    }
    public class ProductRequestDTO
    {
        public int Id { get; set; }
        public int? CategoryID { get; set; }
        public string? SupplierID { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(12, MinimumLength = 8)]
        public string? SKU { get; set; }
        public decimal? Price { get; set; }
        public string? Picture { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
