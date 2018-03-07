using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Moonlay.EntityFrameworkCore.Test.Models;

namespace Com.Moonlay.EntityFrameworkCore.Test.Models
{
    public class UnitDbContext : BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Unit.Com.Moonlay.EFCore.Db;Trusted_Connection=True;");
        }

        public DbSet<StandardEntity> StandardEntities { get; set; }
        public DbSet<StandardCompositeEntity> StandardCompositeEntities { get; set; }
    }
}
