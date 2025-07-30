using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureRelationships(modelBuilder);
            SeedData(modelBuilder);
        }

        private void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // User - Account relationship (One-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Customer relationship (One-to-Many)
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithMany(u => u.Customers)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Manager relationship (One-to-Many)
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.User)
                .WithMany(u => u.Managers)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Staff relationship (One-to-Many)
            modelBuilder.Entity<Staff>()
                .HasOne(s => s.User)
                .WithMany(u => u.Staffs)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Room - Booking relationship (One-to-Many)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer - Booking relationship (One-to-Many)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Manager - Booking relationship (One-to-Many)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Manager)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Room - WorkShift relationship (One-to-Many)
            modelBuilder.Entity<WorkShift>()
                .HasOne(ws => ws.Room)
                .WithMany(r => r.WorkShifts)
                .HasForeignKey(ws => ws.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Staff - WorkShift relationship (One-to-Many)
            modelBuilder.Entity<WorkShift>()
                .HasOne(ws => ws.Staff)
                .WithMany(s => s.WorkShiftList)
                .HasForeignKey(ws => ws.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            // Manager - WorkShift relationship (One-to-Many)
            modelBuilder.Entity<WorkShift>()
                .HasOne(ws => ws.Manager)
                .WithMany(m => m.WorkShifts)
                .HasForeignKey(ws => ws.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Room - Picture relationship (One-to-Many)
            modelBuilder.Entity<Picture>()
                .HasOne(p => p.Room)
                .WithMany()
                .HasForeignKey(p => p.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Manager - Attendance relationship (One-to-Many)
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Manager)
                .WithMany()
                .HasForeignKey(a => a.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure decimal precision
            modelBuilder.Entity<Room>()
                .Property(r => r.PriceForRent)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Room>()
                .Property(r => r.PricePerHour)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasPrecision(18, 2);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { ID = 1, Role = Role.Admin, FullName = "A", CitizenId = "123456789012", DOB = new DateOnly(1985, 5, 15), IsMale = true, AvtUrl = "https://example.com/avatar1.jpg" },
                new User { ID = 2, Role = Role.Manager, FullName = "B", CitizenId = "234567890123", DOB = new DateOnly(1988, 8, 20), IsMale = false, AvtUrl = "https://example.com/avatar2.jpg" },
                new User { ID = 3, Role = Role.Staff, FullName = "C", CitizenId = "345678901234", DOB = new DateOnly(1992, 3, 10), IsMale = true, AvtUrl = "https://example.com/avatar3.jpg" },
                new User { ID = 4, Role = Role.Staff, FullName = "D", CitizenId = "456789012345", DOB = new DateOnly(1990, 12, 5), IsMale = false, AvtUrl = "https://example.com/avatar4.jpg" },
                new User { ID = 5, Role = Role.Customer, FullName = "E", CitizenId = "567890123456", DOB = new DateOnly(1995, 7, 25), IsMale = true, AvtUrl = "https://example.com/avatar5.jpg" },
                new User { ID = 6, Role = Role.Customer, FullName = "F", CitizenId = "678901234567", DOB = new DateOnly(1993, 11, 18), IsMale = false, AvtUrl = "https://example.com/avatar6.jpg" },
                new User { ID = 7, Role = Role.Customer, FullName = "G", CitizenId = "789012345678", DOB = new DateOnly(1987, 4, 30), IsMale = true, AvtUrl = "https://example.com/avatar7.jpg" }
            );

            // Seed Accounts
            modelBuilder.Entity<Account>().HasData(
                new Account { ID = 1, UserName = "admin", Password = "admin123", IsDelete = false, UserId = 1, PhoneNumber = "0123456789", Email = "admin@hotel.com" },
                new Account { ID = 2, UserName = "manager1", Password = "manager123", IsDelete = false, UserId = 2, PhoneNumber = "0987654321", Email = "manager1@hotel.com" },
                new Account { ID = 3, UserName = "staff1", Password = "staff123", IsDelete = false, UserId = 3, PhoneNumber = "0912345678", Email = "staff1@hotel.com" },
                new Account { ID = 4, UserName = "staff2", Password = "staff123", IsDelete = false, UserId = 4, PhoneNumber = "0923456789", Email = "staff2@hotel.com" },
                new Account { ID = 5, UserName = "customer1", Password = "customer123", IsDelete = false, UserId = 5, PhoneNumber = "0934567890", Email = "customer1@gmail.com" },
                new Account { ID = 6, UserName = "customer2", Password = "customer123", IsDelete = false, UserId = 6, PhoneNumber = "0945678901", Email = "customer2@gmail.com" },
                new Account { ID = 7, UserName = "customer3", Password = "customer123", IsDelete = false, UserId = 7, PhoneNumber = "0956789012", Email = "customer3@gmail.com" }
            );

            // Seed Managers
            modelBuilder.Entity<Manager>().HasData(
                new Manager { ID = 1, UserId = 2 }
            );

            // Seed Staff
            modelBuilder.Entity<Staff>().HasData(
                new Staff { ID = 1, UserId = 3 },
                new Staff { ID = 2, UserId = 4 }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { ID = 1, UserId = 5, BookingId = 1 },
                new Customer { ID = 2, UserId = 6, BookingId = 2 },
                new Customer { ID = 3, UserId = 7, BookingId = 3 }
            );

            // Seed Rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { ID = 1, Status = Status.Available, PriceForRent = 500000m, PricePerHour = 50000m, Description = "Phòng đơn tiêu chuẩn với đầy đủ tiện nghi" },
                new Room { ID = 2, Status = Status.Available, PriceForRent = 800000m, PricePerHour = 80000m, Description = "Phòng đôi cao cấp với view đẹp" },
                new Room { ID = 3, Status = Status.Cleanning, PriceForRent = 1200000m, PricePerHour = 120000m, Description = "Phòng suite sang trọng với phòng khách riêng" },
                new Room { ID = 4, Status = Status.Available, PriceForRent = 600000m, PricePerHour = 60000m, Description = "Phòng gia đình rộng rãi" },
                new Room { ID = 5, Status = Status.Rented, PriceForRent = 700000m, PricePerHour = 70000m, Description = "Phòng VIP với jacuzzi" }
            );

            // Seed Pictures
            modelBuilder.Entity<Picture>().HasData(
                new Picture { ID = 1, RoomId = 1, PictureUrl = "https://example.com/room1_1.jpg" },
                new Picture { ID = 2, RoomId = 1, PictureUrl = "https://example.com/room1_2.jpg" },
                new Picture { ID = 3, RoomId = 2, PictureUrl = "https://example.com/room2_1.jpg" },
                new Picture { ID = 4, RoomId = 2, PictureUrl = "https://example.com/room2_2.jpg" },
                new Picture { ID = 5, RoomId = 3, PictureUrl = "https://example.com/room3_1.jpg" },
                new Picture { ID = 6, RoomId = 4, PictureUrl = "https://example.com/room4_1.jpg" },
                new Picture { ID = 7, RoomId = 5, PictureUrl = "https://example.com/room5_1.jpg" }
            );

            // Seed Bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    ID = 1,
                    RoomId = 5,
                    CustomerId = 1,
                    ManagerId = 1,
                    CheckInTime = DateTime.Now.AddDays(-2),
                    CheckOutTime = null,
                    TotalPrice = 1400000m,
                    Status = BookingStatus.Confirm
                },
                new Booking
                {
                    ID = 2,
                    RoomId = 2,
                    CustomerId = 2,
                    ManagerId = 1,
                    CheckInTime = DateTime.Now.AddDays(1),
                    CheckOutTime = DateTime.Now.AddDays(3),
                    TotalPrice = 1600000m,
                    Status = BookingStatus.Pending
                },
                new Booking
                {
                    ID = 3,
                    RoomId = 1,
                    CustomerId = 3,
                    ManagerId = 1,
                    CheckInTime = DateTime.Now.AddDays(-5),
                    CheckOutTime = DateTime.Now.AddDays(-3),
                    TotalPrice = 1000000m,
                    Status = BookingStatus.Confirm
                }
            );

            // Seed WorkShifts
            modelBuilder.Entity<WorkShift>().HasData(
                new WorkShift { ID = 1, RoomId = 1, StaffId = 1, ManagerId = 1, WorkShipStart = new DateTime(2025, 06, 20), WorkShipEnd = new DateTime(2025, 06, 23) },
                new WorkShift { ID = 2, RoomId = 2, StaffId = 2, ManagerId = 1, WorkShipStart = new DateTime(2025, 05, 13), WorkShipEnd = new DateTime(2025, 06, 20) },
                new WorkShift { ID = 3, RoomId = 3, StaffId = 1, ManagerId = 1, WorkShipStart = new DateTime(2025, 04, 13), WorkShipEnd = new DateTime(2025, 05, 20) },
                new WorkShift { ID = 4, RoomId = 4, StaffId = 2, ManagerId = 1, WorkShipStart = new DateTime(2025, 06, 13), WorkShipEnd = new DateTime(2025, 07, 20) },
                new WorkShift { ID = 5, RoomId = 5, StaffId = 1, ManagerId = 1, WorkShipStart = new DateTime(2025, 07, 13), WorkShipEnd = new DateTime(2025, 07, 20) }
            );

            // Seed Attendances
            modelBuilder.Entity<Attendance>().HasData(
                new Attendance
                {
                    ID = 1,
                    WorkShiftStart = DateTime.Today.AddHours(8),
                    WorkShiftEnd = DateTime.Today.AddHours(16),
                    ManagerId = 1
                },
                new Attendance
                {
                    ID = 2,
                    WorkShiftStart = DateTime.Today.AddDays(-1).AddHours(8),
                    WorkShiftEnd = DateTime.Today.AddDays(-1).AddHours(16),
                    ManagerId = 1
                },
                new Attendance
                {
                    ID = 3,
                    WorkShiftStart = DateTime.Today.AddHours(16),
                    WorkShiftEnd = DateTime.Today.AddDays(-1).AddHours(16),
                    ManagerId = 1
                }
            );
        }
    }
}