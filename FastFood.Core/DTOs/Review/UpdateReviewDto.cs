namespace FastFood.Core.DTOs.Review;

public class UpdateReviewDto
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public int FoodItemId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }
}