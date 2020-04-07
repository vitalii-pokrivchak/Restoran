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
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-BU8VU83\\SQLEXPRESS;Initial Catalog = RestoranNew;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
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
                entity.HasData(new Abonent { Id = 1, Name = "Table1" },
                               new Abonent { Id = 2, Name = "Table2" },
                               new Abonent { Id = 3, Name = "Table3" },
                               new Abonent { Id = 4, Name = "Table4" });
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
                entity.HasData(new FoodItem { Id = 1, Name = "Borsh", Price = 20, Count = 2 },
                               new FoodItem { Id = 2, Name = "Chicken Soup", Price = 50, Count = 5 },
                               new FoodItem { Id = 3, Name = "Ice Cream", Price = 25, Count = 4 });
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
                entity.Property(p => p.FixedSource).HasConversion(sourceConverter);
                entity.HasData(new Models.Order { Id = 1, AbonentId = 1, TimeOrder = DateTime.Now, FixedSource = FixSource.Bar ,WaiterId =1},
                    new Models.Order { Id = 2,  AbonentId = 2, TimeOrder = DateTime.Now, FixedSource = FixSource.Bar, WaiterId = 2},
                    new Models.Order { Id = 3, AbonentId = 3, TimeOrder = DateTime.Now, FixedSource = FixSource.Kitchen, WaiterId = 2 });
            });
            modelBuilder.Entity<Detail>(entity =>
            {
                entity.Property(p => p.OrderId).HasColumnName("order_id");
                entity.Property(p => p.ItemsId).HasColumnName("items_id");
                entity.Property(p => p.Bill).HasColumnName("bill");
                entity.Property(p => p.Count).HasColumnName("count");
                entity.Property(p => p.Price).HasColumnName("price");

                entity.HasData(new Detail { Id = 1, ItemsId = 1, Price = 20 },
                               new Detail { Id = 2, ItemsId = 2, Price = 10 },
                               new Detail { Id = 3, ItemsId = 3, Price = 15 });
            });
            modelBuilder.Entity<Waiter>(entity =>
            {
                entity.HasData(new Waiter { Id = 1, Name = "Ivan", Password = "1111" },
                               new Waiter { Id = 2, Name = "Suzana", Password = "2222" },
                               new Waiter { Id = 3, Name = "Andrea", Password = "3333" });
            });
        }

    }
}
