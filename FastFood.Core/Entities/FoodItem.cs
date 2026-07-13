namespace FastFood.Core.Entities
{
    public class FoodItem
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}