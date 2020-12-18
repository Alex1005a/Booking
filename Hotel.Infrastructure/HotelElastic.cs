using AutoMapper;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using System;

namespace HotelSevice.Infrastructure
{
    class HotelElastic
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public bool? Confirmed { get; set; }

        public string[] Tags { get; set; }

        public DateTime CreatedTime { get; set; }

        public Address Address { get; set; }

        public HotelOwner HotelOwner { get; set; }

        public static HotelElastic ToHotelMongo(Hotel hotel)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.DisableConstructorMapping();
                cfg.CreateMap<Hotel, HotelElastic>().ForMember(c => c.PhoneNumber, i => i.MapFrom(g => g.PhoneNumber.Value))
                                                  .ForMember(c => c.Id, i => i.MapFrom(g => g.Id.Value));
            });

            var mapper = config.CreateMapper();

            return mapper.Map<HotelElastic>(hotel);
        }

        public Hotel ToHotel()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.DisableConstructorMapping();
                cfg.CreateMap<HotelElastic, Hotel>().ForMember(c => c.PhoneNumber, i => i.MapFrom(g => new PhoneNumber(g.PhoneNumber)))
                                                  .ForMember(c => c.Id, i => i.MapFrom(g => new HotelId(g.Id)));
            });

            var mapper = config.CreateMapper();

            return mapper.Map<Hotel>(this);
        }
    }
}
