using DiscountGrpc.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options)
       : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, FoodId = "9ab82554-8d8c-4da8-a84b-391d3057d5b8", Description = "IPhone Discount", Amount = 2000 },
            new Coupon { Id = 2, FoodId = "9ab82554-8d8c-4da8-a84b-391d3057d5b8", Description = "Samsung Discount", Amount = 1500 }
            );
    }
}
