using FastFood.Core.Entities;

namespace FastFood.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }

        IFoodRepository FoodItems { get; }

        IGenericRepository<Cart> Carts { get; }

        IGenericRepository<CartItem> CartItems { get; }

        IGenericRepository<Order> Orders { get; }

        IGenericRepository<OrderItem> OrderItems { get; }

        IGenericRepository<Review> Reviews { get; }

        IGenericRepository<Payment> Payments { get; }

        Task<int> CompleteAsync();
    }
}