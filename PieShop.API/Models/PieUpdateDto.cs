using System.ComponentModel.DataAnnotations;

namespace PieShop.API.Models
{
    public class PieUpdateDto
    {
        [Required(ErrorMessage = "A name value is required for updating a pie")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A price value is required for updating a pie")]
        public double Price { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
