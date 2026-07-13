using Microsoft.AspNetCore.Http;

namespace FastFood.Core.DTOs.Food;

public class UpdateFoodItemDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public int CategoryId { get; set; }

    public IFormFile? Image { get; set; }
}