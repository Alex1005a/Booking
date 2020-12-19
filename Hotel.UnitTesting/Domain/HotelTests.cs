using HotelSevice.Domain.AggregatesModel.Exeptions;
using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using System;
using Xunit;

namespace HotelSevice.UnitTesting.Domain
{
    public class HotelTests
    {
        [Fact]
        public void Not_phone_number_exeption()
        {
            string fakePhoneNumber = "not PhoneNumber";
            //Assert
            Assert.Throws<HotelDomainException>(() =>  new Hotel(
                HotelId.Generate(),
                "Hotel",
                "desc",
                new PhoneNumber(fakePhoneNumber),
                DateTime.Now,
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(Guid.NewGuid(), "name", "+020 111 94546 333")
                ));           
        }

        [Fact]
        public void Hotel_id_equeal_test()
        {
            Assert.False(HotelId.Generate() == HotelId.Generate());
            Assert.True(new HotelId(Guid.Empty.ToString()) == new HotelId(Guid.Empty.ToString()));
        }
    }
}
