namespace FastFood.Core.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int FoodItemId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}