namespace FastFood.Core.DTOs.Common;

public class FoodQueryParameters
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsAvailable { get; set; }

    public string? SortBy { get; set; }
}