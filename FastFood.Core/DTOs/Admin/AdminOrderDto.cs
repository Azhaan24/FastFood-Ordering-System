namespace FastFood.Core.DTOs.Admin;

public class AdminOrderDto
{
    public int Id { get; set; }

    public string Customer { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;

    public int TotalItems { get; set; }
}