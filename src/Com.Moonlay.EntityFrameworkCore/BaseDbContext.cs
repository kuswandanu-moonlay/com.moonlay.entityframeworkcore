using System.Linq;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Com.Moonlay.EntityFrameworkCore
{
    public abstract class BaseDbContext : DbContext
    {
        public BaseDbContext() : base() { }
        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ConfigAllEntities(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
              .Where(p => p.State == EntityState.Deleted))
                SoftDelete(entry);


            return base.SaveChanges();
        }

        private void SoftDelete(EntityEntry entry)
        {
            if (entry.Entity is ISoftEntity)
            {
                var entity = entry.Entity as ISoftEntity;
                entity._IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}