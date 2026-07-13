using System.ComponentModel.DataAnnotations;

namespace FastFood.Core.DTOs.Cart
{
    public class UpdateCartQuantityDto
    {
        [Range(1, 20)]
        public int Quantity { get; set; }
    }
}