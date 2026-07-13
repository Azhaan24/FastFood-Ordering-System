using FastFood.Core.Entities;
using FastFood.Core.Interfaces;
using FastFood.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.Repositories
{
    public class FoodRepository : GenericRepository<FoodItem>, IFoodRepository
    {
        public FoodRepository(ApplicationDbContext context)  : base(context)
        {
        }

        public async Task<IEnumerable<FoodItem>> GetFoodByCategoryAsync(int categoryId)
        {
            return await _context.FoodItems
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}