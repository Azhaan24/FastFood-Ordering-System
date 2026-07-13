using System.ComponentModel.DataAnnotations;

namespace FastFood.Core.DTOs.Order
{
    public class UpdateOrderStatusDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}