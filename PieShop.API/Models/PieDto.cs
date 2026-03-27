using System.ComponentModel.DataAnnotations;

namespace PieShop.API.Models
{
    public class PieDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
