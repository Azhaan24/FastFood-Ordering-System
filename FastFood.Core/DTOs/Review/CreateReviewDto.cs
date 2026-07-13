using System.ComponentModel.DataAnnotations;

namespace FastFood.Core.DTOs.Review;

public class CreateReviewDto
{
    [Required]
    public int FoodItemId { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(500)]
    public string? Comment { get; set; }
}