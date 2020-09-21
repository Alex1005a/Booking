﻿using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using HotelSevice.Infrastructure.MongoRepository;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelSevice.Infrastructure
{
    public class HotelRepository : IHotelRepository
    {

        private readonly MongoContext _context;
        private readonly IMongoCollection<Hotel> DbSet;

        public HotelRepository()
        {
            _context = new MongoContext();
            DbSet = _context.GetCollection<Hotel>(typeof(Hotel).Name);
        }

        public IUnitOfWork UnitOfWork => _context;


        public Hotel Add(Hotel hotel)
        {
            _context.AddCommand(async () => await DbSet.InsertOneAsync(hotel));
            return hotel;
        }

        public async Task<Hotel> GetAsync(string hotelId)
        {
            var data = await DbSet.FindAsync(Builders<Hotel>.Filter.Eq(s => s.Id, hotelId));
            return data.FirstOrDefault();
        }

        public void Update(Hotel hotel)
        {
            var filter = Builders<Hotel>.Filter.Eq(s => s.Id, hotel.Id);
            DbSet.ReplaceOne(filter, hotel);
        }
    }
}