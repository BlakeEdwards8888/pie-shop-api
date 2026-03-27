using System.ComponentModel.DataAnnotations;

namespace PieShop.API.Models
{
    public class PieCreationDto
    {
        [Required(ErrorMessage = "A name value is required for the creation of a new pie")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A price value is required for the creation of a new pie")]
        public double Price { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }
    }
}
