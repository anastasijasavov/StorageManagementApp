namespace StorageManagementApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public float PurchasePrice { get; set; }
        public float RetailPrice { get; set; }
        public int InStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Code { get; set; }
        public string ImagePath { get; set; }
    }
}
