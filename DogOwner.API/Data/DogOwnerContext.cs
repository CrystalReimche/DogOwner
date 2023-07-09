using DogOwner.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DogOwner.API.Data
{
    public class DogOwnerContext : DbContext
    {
        public DogOwnerContext(DbContextOptions<DogOwnerContext> options) : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Dogs)
                .WithOne(d => d.Owner)
                .HasForeignKey(d => d.OwnerId);
        }
    }
}
