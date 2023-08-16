using ReactQuery_Server.Data.Models.Enums;

namespace ReactQuery_Server.Shared.Products
{
    public class ProductById 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }
    }
}
