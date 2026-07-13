using System.ComponentModel.DataAnnotations;

namespace FastFood.Core.DTOs.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}