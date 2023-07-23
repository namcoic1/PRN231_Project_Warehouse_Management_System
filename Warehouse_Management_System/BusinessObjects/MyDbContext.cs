using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public class MyDbContext : DbContext
    {
        //// static object of mydbcontext
        //private static MyDbContext _instance = null;
        //// safety object 
        //private static readonly object instanceLock = new object();
        //private MyDbContext() { }
        public MyDbContext() { }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        //// singleton pattern
        //public static MyDbContext GetInstance
        //{
        //    get
        //    {
        //        lock (instanceLock)
        //        {
        //            if (_instance == null)
        //            {
        //                _instance = new MyDbContext();
        //            }
        //            return _instance;
        //        }
        //    }
        //}

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
            // connect DB
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("Database"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent API
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inventory>(c =>
            {
                //c.HasOne(c => c.User)
                //.WithMany(c => c.Inventories)
                //.HasForeignKey(c => c.UserId)
                //.OnDelete(DeleteBehavior.Cascade);

                c.HasOne(c => c.Location)
                .WithMany(c => c.Inventories)
                .HasForeignKey(c => c.LocationID);

                c.HasOne(c => c.Product)
                .WithMany(c => c.Inventories)
                .HasForeignKey(c => c.ProductID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // cascade or setnull
            //modelBuilder.Entity<Location>(c =>
            //{
            //    c.HasOne(c => c.User)
            //    .WithOne(c => c.Location)
            //    .HasForeignKey<Location>(c => c.UserID)
            //    .OnDelete(DeleteBehavior.SetNull);
            //});

            modelBuilder.Entity<User>(c =>
            {
                c.HasOne(e => e.Role)
                .WithMany(a => a.Users)
                .HasForeignKey(a => a.RoleID);

                // one to many
                c.HasMany(e => e.Users)
                .WithOne(e => e.Manager)
                .HasForeignKey(e => e.ReportsTo);
                //.OnDelete(DeleteBehavior.SetNull);

                // one to one
                c.HasOne(e => e.Location)
                .WithOne(a => a.User)
                .HasForeignKey<Location>(a => a.UserID)
                .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Product>(c =>
            {
                c.HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

                c.HasOne(c => c.Supplier)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.SupplierID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Report>(c =>
            {
                c.HasOne(c => c.Inventory)
                .WithMany(c => c.Reports)
                .HasForeignKey(c => c.InventoryID);

                c.HasOne(c => c.Transaction)
                .WithMany(c => c.Reports)
                .HasForeignKey(c => c.TransactionID)
                .OnDelete(DeleteBehavior.Cascade);

                c.HasOne(c => c.User)
                .WithMany(c => c.Reports)
                .HasForeignKey(c => c.UserID);
            });

            modelBuilder.Entity<Transaction>(c =>
            {
                c.HasOne(c => c.Carrier)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.CarrierID)
                .OnDelete(DeleteBehavior.Cascade);

                c.HasOne(c => c.Customer)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

                c.HasOne(c => c.Location)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.LocationID);

                c.HasOne(c => c.Product)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

                c.HasOne(c => c.Supplier)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.SupplierID);

                c.HasOne(c => c.User)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
