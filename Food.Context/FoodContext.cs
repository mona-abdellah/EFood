using Food.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Context
{
    public class FoodContext:IdentityDbContext<Customer,IdentityRole<Guid>,Guid>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Category> Categories { get; set; }
        public FoodContext(DbContextOptions<FoodContext> options)
            : base(options)
        {

        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedBy = 1;
                    entity.Entity.Create = DateTime.UtcNow;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.Updated = DateTime.UtcNow;
                    entity.Entity.UpdatedBy = 1;
                }
            }
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity<int>>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedBy = 1;
                    entity.Entity.Create = DateTime.Now;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.Updated = DateTime.Now;
                    entity.Entity.UpdatedBy = 1;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
