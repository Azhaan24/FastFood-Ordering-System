using FastFood.Core.Entities;
using FastFood.Core.Interfaces;
using FastFood.Infrastructure.Data;

namespace FastFood.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository Categories { get; }

        public IFoodRepository FoodItems { get; }

        public IGenericRepository<Cart> Carts { get; }

        public IGenericRepository<CartItem> CartItems { get; }

        public IGenericRepository<Order> Orders { get; }

        public IGenericRepository<OrderItem> OrderItems { get; }

        public IGenericRepository<Review> Reviews { get; }

        public IGenericRepository<Payment> Payments {  get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Categories = new CategoryRepository(context);

            FoodItems = new FoodRepository(context);

            Carts = new GenericRepository<Cart>(context);

            CartItems = new GenericRepository<CartItem>(context);

            Orders = new GenericRepository<Order>(context);

            OrderItems = new GenericRepository<OrderItem>(context);

            Reviews = new GenericRepository<Review>(context);

            Payments = new GenericRepository<Payment>(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}