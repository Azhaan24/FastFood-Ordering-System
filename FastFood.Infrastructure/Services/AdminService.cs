using FastFood.Core.DTOs.Admin;
using FastFood.Core.DTOs.Order;
using FastFood.Core.Entities;
using FastFood.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace FastFood.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(UserManager<ApplicationUser> userManager,ApplicationDbContext context,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task ChangeRoleAsync(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, roles);

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            await _userManager.DeleteAsync(user);
        }

        public async Task<AdminOrderDto?> GetOrderAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Where(o => o.Id == id)
                .Select(o => new AdminOrderDto
                {
                    Id = o.Id,
                    Customer = o.User.FullName,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderDate = o.OrderDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<AdminOrderDto>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new AdminOrderDto
                {
                    Id = o.Id,
                    Customer = o.User.FullName,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderDate = o.OrderDate
                })
                .ToListAsync();
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var result = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new UserDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email!,
                    Role = roles.FirstOrDefault() ?? "User"
                });
            }

            return result;
        }

        public async Task UpdateOrderStatusAsync(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                throw new Exception("Order not found");

            order.Status = status;

            await _context.SaveChangesAsync();
        }

        public async Task<List<AdminOrderDto>> GetOrdersByStatusAsync(string status)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Where(o => o.Status == status)
                .Select(o => new AdminOrderDto
                {
                    Id = o.Id,
                    Customer = o.User.FullName,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderDate = o.OrderDate
                })
                .ToListAsync();
        }

        public async Task<List<TopFoodDto>> GetTopSellingFoodsAsync()
        {
            return await _context.OrderItems
                .Include(x => x.FoodItem)
                .GroupBy(x => x.FoodItem.Name)
                .Select(g => new TopFoodDto
                {
                    FoodName = g.Key,
                    QuantitySold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToListAsync();
        }
    }
}
