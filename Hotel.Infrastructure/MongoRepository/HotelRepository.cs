using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using HotelSevice.Infrastructure.MongoRepository;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Seedwork;
using MongoDB.Bson;
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
        private readonly IMongoCollection<HotelMongo> DbSet;

        public HotelRepository()
        {
            _context = new MongoContext();
            DbSet = _context.GetCollection<HotelMongo>(typeof(HotelMongo).Name);
        }

        public IUnitOfWork UnitOfWork => _context;


        public Hotel Add(Hotel hotel)
        {
            _context.AddCommand(async () => await DbSet.InsertOneAsync(HotelMongo.ToHotelMongo(hotel)));
            return hotel;
        }

        public async Task<Hotel> GetAsync(HotelId hotelId)
        {
            var data = await DbSet.FindAsync(Builders<HotelMongo>.Filter.Eq(s => s.Id, hotelId.Value));
            return data.FirstOrDefault().ToHotel();
        }

        public void Update(Hotel hotel)
        {
            var filter = Builders<HotelMongo>.Filter.Eq(s => s.Id, hotel.Id.Value);
            DbSet.ReplaceOne(filter, HotelMongo.ToHotelMongo(hotel));
        }
    }
}
