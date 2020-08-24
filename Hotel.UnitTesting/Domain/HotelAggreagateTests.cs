using Hotel.Domain.AggregatesModel.Exeptions;
using Hotel.Domain.AggregatesModel.HotelAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.UnitTesting.Domain
{
    public class HotelAggreagateTests
    {
        [Fact]
        public void Not_phone_number_exeption()
        {
            string fakePhoneNumber = "not PhoneNumber";
            //Assert
            Assert.Throws<HotelDomainException>(() =>  new HotelAggregate(
                "Hotel",
                "desc",
                fakePhoneNumber,
                new Address(1, "street", "city", "state", "country"),
                new HotelOwner(Guid.NewGuid(), "name", "+020 111 94546 333")
                ));           
        }
    }
}
