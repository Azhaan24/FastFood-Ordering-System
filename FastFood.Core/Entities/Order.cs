using FastFood.Core.Constants;
using FastFood.Core.Entities;

namespace FastFood.Core.Entities;

public class Order
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } =OrderStatus.Pending;

    public string DeliveryAddress { get; set; } = string.Empty;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}