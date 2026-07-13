namespace FastFood.Core.DTOs.Dashboard;

public class RecentOrderDto
{
    public int OrderId { get; set; }

    public string Customer { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }
}