using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace asm.Models
{
    public class fullContext:DbContext
    {
        public fullContext()
        {

        }

        public fullContext( DbContextOptions<fullContext> options) : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<cartItem> cartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CartDetail>().HasKey(table => new {
                table.ProductId,
                table.CartId
            });
        }
    }
}
