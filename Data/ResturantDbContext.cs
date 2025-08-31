using Microsoft.EntityFrameworkCore;
using ResturantRESTAPI.Models;

namespace ResturantRESTAPI.Data
{
    public class ResturantDbContext : DbContext
    {
        public ResturantDbContext(DbContextOptions<ResturantDbContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Unique index
            var adminEntity = modelBuilder.Entity<Admin>();
            adminEntity.HasIndex(admin => new { admin.Username, admin.PasswordHash }).IsUnique();
            var customerEntity = modelBuilder.Entity<Customer>();
            customerEntity.HasIndex(customer => customer.PhoneNumber).IsUnique();


            modelBuilder.Entity<Booking>()
    .HasOne(b => b.Customer)
    .WithMany(c => c.Bookings)
    .HasForeignKey(b => b.FK_Customer);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Table)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.FK_Table);

            //seed some mock data
            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, Username = "admin", PasswordHash = "password" } //todo add bcrypt
            );

            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Capacity = 2 },
                new Table { Id = 2, Capacity = 4 },
                new Table { Id = 3, Capacity = 4 },
                new Table { Id = 4, Capacity = 6 },
                new Table { Id = 5, Capacity = 8 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Bonnie", PhoneNumber = "1234567890" },
                new Customer { Id = 2, Name = "Clyde", PhoneNumber = "2345678901" },
                new Customer { Id = 3, Name = "Dillinger", PhoneNumber = "3456789012" },
                new Customer { Id = 4, Name = "Nitti", PhoneNumber = "4567890123" }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking { Id = 1, FK_Customer = 1, FK_Table = 1, StartTime = new DateTime(2025, 02, 21, 12, 0, 0), NumberOfGuests = 2 },
                new Booking { Id = 2, FK_Customer = 2, FK_Table = 2, StartTime = new DateTime(2025, 08, 01, 12, 0, 0), NumberOfGuests = 4 }
                );
        }
    }
}
