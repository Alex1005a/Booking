using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Infrastructure
{
    public class TestDbContext : DbContext, IUnitOfWork
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
        { }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd().HasConversion(
            v => v.Value,
            v => new HotelId(v));
            modelBuilder.Entity<Hotel>()
                .OwnsOne(p => p.HotelOwner);
            modelBuilder.Entity<Hotel>()
                .OwnsOne(p => p.Address);
            modelBuilder.Entity<Hotel>()
                .OwnsOne(p => p.PhoneNumber);
            modelBuilder.Entity<Hotel>().Ignore(t => t.DomainEvents);
            modelBuilder.Entity<Hotel>().Property(p => p.Tags)
            .HasConversion(
                v => string.Join("'", v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            base.OnModelCreating(modelBuilder);
        }
    }
}
