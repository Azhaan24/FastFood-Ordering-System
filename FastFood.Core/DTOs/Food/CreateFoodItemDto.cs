using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Core.DTOs.Food;

public class CreateFoodItemDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(1, 10000)]
    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public int CategoryId { get; set; }

    public IFormFile? Image { get; set; }
}