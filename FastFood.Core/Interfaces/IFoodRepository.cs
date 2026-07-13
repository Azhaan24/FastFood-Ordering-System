using FastFood.Core.Entities;

namespace FastFood.Core.Interfaces
{
    public interface IFoodRepository : IGenericRepository<FoodItem>
    {
        Task<IEnumerable<FoodItem>> GetFoodByCategoryAsync(int categoryId);
    }
}