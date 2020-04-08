using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestoranClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestoranClient.Data
{
    public class RestoranDbContext:DbContext
    {
        public DbSet<Waiter>  Waiters { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Abonent> Abonent { get; set; }
        public DbSet<SourceItem> Sources { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Detail> Details { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Config.Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Abonent>(entity =>
            {
                entity.ToTable("abonent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.HasData(new Abonent { Id = 1, Name = "table 1" },
                               new Abonent { Id = 2, Name = "table 2" },
                               new Abonent { Id = 3, Name = "table 3" },
                               new Abonent { Id = 4, Name = "table 4" });

            });
            modelBuilder.Entity<ClientCards>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.ToTable("items");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 2)");

                entity.HasData(new FoodItem { Id = 1, Name = "Bear", Price = 45.00M, Count = 0 },
                               new FoodItem { Id = 2, Name = "Borch", Price = 50.00M, Count = 0 },
                               new FoodItem { Id = 3, Name = "bread", Price = 5.00M, Count = 0 },
                               new FoodItem { Id = 4, Name = "Chicken soup", Price = 45.00M, Count = 0 },
                               new FoodItem { Id = 5, Name = "chicken with poatoes", Price = 95.00M, Count = 0 },
                               new FoodItem { Id = 6, Name = "Coca Cola", Price = 40.00M, Count = 0 },
                               new FoodItem { Id = 7, Name = "Cofee", Price = 25.00M, Count = 0 },
                               new FoodItem { Id = 8, Name = "Duck soup", Price = 45.00M, Count = 0 },
                               new FoodItem { Id = 9, Name = "IceCream", Price = 50.00M, Count = 0 },
                               new FoodItem { Id = 10, Name = "ketchup", Price = 300.00M, Count = 0 },
                               new FoodItem { Id = 11, Name = "pizza", Price = 95.00M, Count = 0 },
                               new FoodItem { Id = 12, Name = "Salad", Price = 100.00M, Count = 0 },
                               new FoodItem { Id = 13, Name = "shashlik", Price = 300.00M, Count = 0 },
                               new FoodItem { Id = 14, Name = "Vodka", Price = 300.00M, Count = 0 },
                               new FoodItem { Id = 15, Name = "water", Price = 40.00M, Count = 0 },
                               new FoodItem { Id = 16, Name = "Whiskey", Price = 1000.00M, Count = 0 });
            });

            var sourceConverter = new ValueConverter<FixSource, string>(
                    v => v.ToString(),
                    v => (FixSource)Enum.Parse(typeof(FixSource), v));
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(p => p.WaiterId).HasColumnName("waiter_id");
                entity.Property(p => p.AbonentId).HasColumnName("abonent_id");
                entity.Property(p => p.TimeOrder).HasColumnName("time_order");
                entity.Property(p => p.EndOrder).HasColumnName("end_order");
                entity.Property(p => p.SourceId).HasColumnName("source_id");
                entity.Property(p => p.Paid).HasColumnName("paid");
                entity.Property(p => p.FixedSource).HasConversion(sourceConverter);

                entity.HasData(new Order { TimeOrder = DateTime.Parse("2020 - 02 - 24 16:00:00.000"), Bill = 290.00M, Id = 1, WaiterId = 2, AbonentId = 1, FixedSource = FixSource.Kitchen, Paid = 0 },
                               new Order { TimeOrder = DateTime.Parse("2020 - 02 - 24 16:00:00.000"), Bill = 140.00M, Id = 4, WaiterId = 1, AbonentId = 3, FixedSource = FixSource.Kitchen, Paid = 0 },
                               new Order { TimeOrder = DateTime.Parse("2020 - 02 - 24 16:05:00.000"), Bill = 125.00M, Id = 5, WaiterId = 1, AbonentId = 4, FixedSource = FixSource.Bar, Paid = 0 },
                               new Order { TimeOrder = DateTime.Parse("2020 - 02 - 24 16:10:00.000"), Bill = 485.00M, Id = 2, WaiterId = 2, AbonentId = 2, FixedSource = FixSource.Kitchen, Paid = 0 },
                               new Order { TimeOrder = DateTime.Parse("2020 - 02 - 24 16:10:00.000"), Bill = 302.50M, Id = 3, WaiterId = 2, AbonentId = 2, FixedSource = FixSource.Bar, Paid = 0 },
                               new Order { TimeOrder = DateTime.Parse("2020 - 02 - 24 16:30:00.000"), Bill = 200.00M, Id = 7, WaiterId = 2, AbonentId = 1, FixedSource = FixSource.Kitchen, Paid = 0 },
                               new Order { TimeOrder = DateTime.Parse("2020 - 02 - 24 16:50:00.000"), Bill = 160.00M, Id = 6, WaiterId = 1, AbonentId = 3, FixedSource = FixSource.Bar, Paid = 0 },
                               new Order { TimeOrder = DateTime.Parse("2020 - 04 - 08 15:16:05.357"), Bill = 100.00M, Id = 8, WaiterId = 1, AbonentId = 1, FixedSource = FixSource.Kitchen, Paid = 0 },
                               new Order { TimeOrder = DateTime.Parse("2020 - 04 - 08 15:16:17.813"), Bill = 10350.00M, Id = 9, WaiterId = 1, AbonentId = 4, FixedSource = FixSource.Bar, Paid = 0 });
            });
            modelBuilder.Entity<Detail>(entity =>
            {
                entity.Property(p => p.OrderId).HasColumnName("order_id");
                entity.Property(p => p.ItemsId).HasColumnName("items_id");
                entity.Property(p => p.Bill).HasColumnName("bill");
                entity.Property(p => p.Count).HasColumnName("count");
                entity.Property(p => p.Price).HasColumnName("price");

                entity.HasData(new Detail { Id = 1, ItemsId = 2, OrderId = 1, Price = 50.00M, Count = 2.000M, Bill = 100.00M },
                               new Detail { Id = 2, ItemsId = 8, OrderId = 2, Price = 45.00M, Count = 2.000M, Bill = 90.00M },
                               new Detail { Id = 3, ItemsId = 14, OrderId = 3, Price = 300.00M, Count = 0.500M, Bill = 150.00M },
                               new Detail { Id = 4, ItemsId = 4, OrderId = 4, Price = 45.00M, Count = 1.000M, Bill = 45.00M },
                               new Detail { Id = 5, ItemsId = 7, OrderId = 5, Price = 25.00M, Count = 2.000M, Bill = 50.00M },
                               new Detail { Id = 6, ItemsId = 7, OrderId = 5, Price = 25.00M, Count = 3.000M, Bill = 75.00M },
                               new Detail { Id = 7, ItemsId = 2, OrderId = 8, Price = 50.00M, Count = 2.000M, Bill = 100.00M },
                               new Detail { Id = 8, ItemsId = 7, OrderId = 9, Price = 25.00M, Count = 2.000M, Bill = 50.00M },
                               new Detail { Id = 9, ItemsId = 15, OrderId = 9, Price = 40.00M, Count = 10.000M, Bill = 400.00M },
                               new Detail { Id = 10, ItemsId = 13, OrderId = 9, Price = 300.00M, Count = 10.000M, Bill = 3000.00M },
                               new Detail { Id = 11, ItemsId = 16, OrderId = 9, Price = 1000.00M, Count = 5.000M, Bill = 5000.00M },
                               new Detail { Id = 12, ItemsId = 11, OrderId = 9, Price = 95.00M, Count = 20.000M, Bill = 1900.00M });
            });

            modelBuilder.Entity<Waiter>(entity =>
            {
                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Name).HasColumnName("name");
                entity.Property(p => p.Password).HasColumnName("password");
                entity.HasData(new Waiter { Id = 1, Name = "Andrea", Password = "1111" },
                               new Waiter { Id = 2, Name = "Suzane", Password = "2222" },
                               new Waiter { Id = 3, Name = "Ivanka", Password = "3333" });
            });
        }

    }
}