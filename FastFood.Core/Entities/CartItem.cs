namespace FastFood.Core.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; } = null!;

        public int FoodItemId { get; set; }

        public FoodItem FoodItem { get; set; } = null!;

        public int Quantity { get; set; }
    }
}