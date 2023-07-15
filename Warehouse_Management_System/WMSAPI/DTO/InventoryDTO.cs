namespace WMSAPI.DTO
{
    public class InventoryDTO
    {
        public string Id { get; set; }
        public int? LocationID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual LocationDTO? Location { get; set; }
        public virtual ProductDTO? Product { get; set; }
    }
    public class InventoryRequestDTO
    {
        public string Id { get; set; }
        public int? LocationID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
