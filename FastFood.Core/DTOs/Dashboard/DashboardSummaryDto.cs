namespace FastFood.Core.DTOs.Dashboard;

public class DashboardSummaryDto
{
    public int TotalUsers { get; set; }

    public int TotalOrders { get; set; }

    public int TotalFoodItems { get; set; }

    public decimal TotalRevenue { get; set; }
}