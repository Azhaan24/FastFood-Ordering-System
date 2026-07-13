namespace FastFood.Core.DTOs.Cart
{
    public class CartItemDto
    {
        public int Id { get; set; }

        public int FoodItemId { get; set; }

        public string FoodName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }
}