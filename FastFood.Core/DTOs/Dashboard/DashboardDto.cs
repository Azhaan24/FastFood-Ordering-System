namespace FastFood.Core.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalUsers { get; set; }

        public int TotalFoodItems { get; set; }

        public int TotalCategories { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public int PendingOrders { get; set; }

        public int CompletedOrders { get; set; }

        public int TotalReviews { get; set; }
    }
}