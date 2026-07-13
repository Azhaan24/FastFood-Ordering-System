using FastFood.Core.DTOs.Common;
using FastFood.Core.DTOs.Food;

namespace FastFood.Core.Interfaces;

public interface IFoodItemService
{
    Task<PagedResponse<FoodItemDto>> GetAllAsync(FoodQueryParameters query);

    Task<FoodItemDto?> GetByIdAsync(int id);

    Task<FoodItemDto> CreateAsync(CreateFoodItemDto dto);

    Task<bool> UpdateAsync(int id, UpdateFoodItemDto dto);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<FoodItemDto>> GetByCategoryAsync(int categoryId);

    Task<List<FoodItemDto>> SearchAsync(string keyword);

    Task<IEnumerable<FoodItemDto>> GetAvailableAsync();
}