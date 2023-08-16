using System.ComponentModel.DataAnnotations;

namespace ReactQuery_Server.Shared.Products
{
    public class BaseInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
