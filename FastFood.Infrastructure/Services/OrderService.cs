using AutoMapper;
using FastFood.Core.DTOs.Order;
using FastFood.Core.Entities;
using FastFood.Core.Interfaces;

namespace FastFood.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(string userId)
    {
        var orders = await _unitOfWork.Orders.FindAsync(o => o.UserId == userId);

        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetOrderAsync(int orderId)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order == null)
            return null;

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> CreateOrderAsync(
        string userId,
        CreateOrderDto dto)
    {
        var cart = (await _unitOfWork.Carts.FindAsync(c => c.UserId == userId))
            .FirstOrDefault();

        if (cart == null)
            throw new Exception("Cart not found.");

        var cartItems = await _unitOfWork.CartItems.FindAsync(x => x.CartId == cart.Id);

        if (!cartItems.Any())
            throw new Exception("Cart is empty.");

        var order = new Order
        {
            UserId = userId,
            DeliveryAddress = dto.DeliveryAddress,
            OrderDate = DateTime.UtcNow,
            Status = "Pending"
        };

        decimal total = 0;

        foreach (var item in cartItems)
        {
            var food = await _unitOfWork.FoodItems.GetByIdAsync(item.FoodItemId);

            if (food == null)
                continue;

            total += food.Price * item.Quantity;

            order.OrderItems.Add(new OrderItem
            {
                FoodItemId = food.Id,
                Quantity = item.Quantity,
                UnitPrice = food.Price
            });
        }

        order.TotalAmount = total;

        await _unitOfWork.Orders.AddAsync(order);

        foreach (var item in cartItems)
        {
            _unitOfWork.CartItems.Delete(item);
        }

        _unitOfWork.Carts.Delete(cart);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<bool> CancelOrderAsync(int orderId)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order == null)
            return false;

        order.Status = "Cancelled";

        _unitOfWork.Orders.Update(order);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _unitOfWork.Orders.GetAllAsync();

        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<bool> UpdateOrderStatusAsync(
        int orderId,
        string status)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);

        if (order == null)
            return false;

        order.Status = status;

        _unitOfWork.Orders.Update(order);

        await _unitOfWork.CompleteAsync();

        return true;
    }
}