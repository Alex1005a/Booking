using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Domain.AggregatesModel.Exeptions
{
    public class HotelDomainException : Exception
    {
        public HotelDomainException()
        { }

        public HotelDomainException(string message)
            : base(message)
        { }

        public HotelDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
