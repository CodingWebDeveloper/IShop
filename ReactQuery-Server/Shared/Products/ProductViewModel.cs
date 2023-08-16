namespace ReactQuery_Server.Shared.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
        
        public bool IsFavorite { get; set; }
    }
}
