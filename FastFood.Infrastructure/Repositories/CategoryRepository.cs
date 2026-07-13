using FastFood.Core.Entities;
using FastFood.Core.Interfaces;
using FastFood.Infrastructure.Data;

namespace FastFood.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}