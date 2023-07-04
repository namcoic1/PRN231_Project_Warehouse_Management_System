using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public class MyDB_Context : DbContext
    {
        public MyDB_Context()
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("Database"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // one to one
            modelBuilder.Entity<User>()
            .HasOne(e => e.Location)
            .WithOne(a => a.User)
            .HasForeignKey<Location>(a => a.UserID);

            // cascade or setnull
            // one to many
            modelBuilder.Entity<User>()
            .HasMany(e => e.Users)
            .WithOne(e => e.Manager)
            .HasForeignKey(e => e.ReportsTo);
            //.OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Inventory>()
            //    .HasOne(p => p.Product)
            //    .WithMany(i => i.Inventories)
            //    .HasForeignKey(i => i.ProductID)
            //    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
