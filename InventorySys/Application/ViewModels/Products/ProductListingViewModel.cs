namespace Application.ViewModels.Products
{
    public class ProductListingViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public long CategoryId { get; set; }
        public string Status { get; set; }
        public bool IsWeighted { get; set; }
    }
}
