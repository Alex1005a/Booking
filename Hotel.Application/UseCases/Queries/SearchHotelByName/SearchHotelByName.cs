using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System.Collections.Generic;

namespace HotelSevice.Application.UseCases.Queries.SearchHotelByName
{
    public class SearchHotelByName : IRequest<IEnumerable<Hotel>>
    {
        public string Name { get; set; }

        public int Page { get; set; }

        public SearchHotelByName(string name, int page)
        {
            Name = name;
            Page = page;
        }
    }
}
