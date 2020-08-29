using Hotel.Domain.AggregatesModel.HotelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Infrastructure
{
    public class TestDbContext : DbContext, IUnitOfWork
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
        { }

        public DbSet<HotelAggregate> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelAggregate>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<HotelAggregate>()
                .OwnsOne(p => p.HotelOwner);
            modelBuilder.Entity<HotelAggregate>()
                .OwnsOne(p => p.Address);
            modelBuilder.Entity<HotelAggregate>().Ignore(t => t.DomainEvents);

            base.OnModelCreating(modelBuilder);
        }
    }
}
