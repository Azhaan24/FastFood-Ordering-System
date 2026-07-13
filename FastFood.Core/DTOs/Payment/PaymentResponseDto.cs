namespace FastFood.Core.DTOs.Payment;

public class PaymentResponseDto
{
    public string OrderId { get; set; } = "";

    public string RazorpayOrderId { get; set; } = "";

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "INR";

    public string Key { get; set; } = "";
}