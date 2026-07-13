namespace FastFood.Core.Entities;

public class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Order? Order { get; set; }

    public string RazorpayOrderId { get; set; } = "";

    public string RazorpayPaymentId { get; set; } = "";

    public decimal Amount { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; }
}