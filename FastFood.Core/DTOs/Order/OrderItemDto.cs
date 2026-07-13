namespace FastFood.Core.DTOs.Order;

public class OrderItemDto
{
    public int FoodItemId { get; set; }

    public string FoodName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => Quantity * UnitPrice;
}