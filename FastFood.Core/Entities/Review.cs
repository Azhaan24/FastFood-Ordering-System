namespace FastFood.Core.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int FoodItemId { get; set; }

        public FoodItem? FoodItem { get; set; }

        public string UserId { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}