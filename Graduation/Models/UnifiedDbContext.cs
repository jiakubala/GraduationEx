using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    public class UnifiedDbContext : DbContext
    {
        public UnifiedDbContext() { }

        public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Good> Good { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(a =>
            {
                a.ToTable("user");

                a.Property(e => e.UserId)
                .HasColumnName("userid")
                .HasColumnType("int(11)");

                a.Property(e => e.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(11)");

                a.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(11)");

                a.Property(e => e.Sex)
                .HasColumnName("sex")
                .HasColumnType("varchar(11)");

                a.Property(e => e.TrueName)
                .HasColumnName("truename")
                .HasColumnType("varchar(5)");

                a.Property(e => e.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(20)");

                a.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasColumnType("int(11)");
            });

            builder.Entity<Good>(a =>
            {
                a.ToTable("good");

                a.Property(e => e.GoodId)
                .HasColumnName("goodid")
                .HasColumnType("int(11)");

                a.Property(e => e.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(11)");

                a.Property(e => e.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10)");

                a.Property(e => e.Stock)
                .HasColumnName("stock")
                .HasColumnType("int(255)");

                a.Property(e => e.Sold)
                .HasColumnName("sold")
                .HasColumnType("int(255)");

                a.Property(e => e.Details)
                .HasColumnName("details")
                .HasColumnType("varchar(255)");

                a.Property(e => e.Type)
                .HasColumnName("type")
                .HasColumnType("varchar(50)");
            });

            builder.Entity<Order>(a =>
            {
                a.ToTable("order");

                a.Property(e => e.OrderId)
                .HasColumnName("orderid")
                .HasColumnType("int(11)");

                a.Property(e => e.UserId)
                .HasColumnName("userid")
                .HasColumnType("int(11)");

                a.Property(e => e.GoodId)
                .HasColumnName("goodid")
                .HasColumnType("int(11)");

                a.Property(e => e.OrderState)
                .HasColumnName("orderstate")
                .HasColumnType("int(4)");

                a.Property(e => e.GoodNumber)
                .HasColumnName("goodnumber")
                .HasColumnType("int(11)");
            });

        }
    }
}
