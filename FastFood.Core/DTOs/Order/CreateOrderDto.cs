using System.ComponentModel.DataAnnotations;

namespace FastFood.Core.DTOs.Order;

public class CreateOrderDto
{
    [Required]
    public string DeliveryAddress { get; set; } = string.Empty;
}