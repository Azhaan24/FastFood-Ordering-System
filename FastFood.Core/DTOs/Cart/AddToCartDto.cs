using System.ComponentModel.DataAnnotations;

namespace FastFood.Core.DTOs.Cart
{
    public class AddToCartDto
    {
        [Required]
        public int FoodItemId { get; set; }

        [Range(1, 20)]
        public int Quantity { get; set; }
    }
}