using ReactQuery_Server.Data.Models.Enums;

namespace ReactQuery_Server.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public bool isFavorite { get; set; }

    }
}
