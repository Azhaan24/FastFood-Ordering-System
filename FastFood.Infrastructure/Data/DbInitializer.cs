using FastFood.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FastFood.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await context.Database.EnsureCreatedAsync();

        // Roles
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("Customer"))
            await roleManager.CreateAsync(new IdentityRole("Customer"));

        // Admin User
        var admin = await userManager.FindByEmailAsync("admin@fastfood.com");

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = "admin@fastfood.com",
                Email = "admin@fastfood.com",
                FullName = "System Admin",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(admin, "Admin@123");

            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Categories
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Pizza" },
                new Category { Name = "Burger" },
                new Category { Name = "Drinks" },
                new Category { Name = "Desserts" }
            );

            await context.SaveChangesAsync();
        }

        // Food Items
        if (!context.FoodItems.Any())
        {
            var pizza = context.Categories.First(x => x.Name == "Pizza");
            var burger = context.Categories.First(x => x.Name == "Burger");
            var drinks = context.Categories.First(x => x.Name == "Drinks");

            context.FoodItems.AddRange(

                new FoodItem
                {
                    Name = "Margherita Pizza",
                    Description = "Classic cheese pizza",
                    Price = 299,
                    CategoryId = pizza.Id,
                    IsAvailable = true
                },

                new FoodItem
                {
                    Name = "Veg Burger",
                    Description = "Loaded veggie burger",
                    Price = 149,
                    CategoryId = burger.Id,
                    IsAvailable = true
                },

                new FoodItem
                {
                    Name = "Cold Coffee",
                    Description = "Chilled coffee",
                    Price = 99,
                    CategoryId = drinks.Id,
                    IsAvailable = true
                }

            );

            await context.SaveChangesAsync();
        }
    }
}