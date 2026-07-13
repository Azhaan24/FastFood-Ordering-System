using FastFood.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await context.Database.MigrateAsync();

        await SeedRoles(roleManager);

        await SeedAdmin(userManager);

        await SeedCategories(context);

        await SeedFoodItems(context);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roles =
        {
            "Admin",
            "Customer"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(role));
            }
        }
    }

    private static async Task SeedAdmin(
        UserManager<ApplicationUser> userManager)
    {
        const string email = "admin@fastfood.com";

        var admin = await userManager.FindByEmailAsync(email);

        if (admin != null)
            return;

        admin = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = "System Administrator",
            EmailConfirmed = true
        };

        await userManager.CreateAsync(admin, "Admin@123");

        await userManager.AddToRoleAsync(admin, "Admin");
    }

    private static async Task SeedCategories(ApplicationDbContext context)
    {
        if (context.Categories.Any())
            return;

        context.Categories.AddRange(
            new Category { Name = "Pizza" },
            new Category { Name = "Burger" },
            new Category { Name = "Drinks" },
            new Category { Name = "Desserts" });

        await context.SaveChangesAsync();
    }

    private static async Task SeedFoodItems(ApplicationDbContext context)
    {
        if (context.FoodItems.Any())
            return;

        context.FoodItems.AddRange(

            new FoodItem
            {
                Name = "Margherita Pizza",
                Description = "Cheese Pizza",
                Price = 299,
                CategoryId = 1,
                ImageUrl = "",
                IsAvailable = true
            },

            new FoodItem
            {
                Name = "Veg Burger",
                Description = "Fresh Veg Burger",
                Price = 149,
                CategoryId = 2,
                ImageUrl = "",
                IsAvailable = true
            },

            new FoodItem
            {
                Name = "Cold Coffee",
                Description = "Iced Coffee",
                Price = 120,
                CategoryId = 3,
                ImageUrl = "",
                IsAvailable = true
            }

        );

        await context.SaveChangesAsync();
    }
}