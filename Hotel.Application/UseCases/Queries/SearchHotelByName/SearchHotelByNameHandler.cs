using HotelSevice.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Application.UseCases.Queries.SearchHotelByName
{
    public class SearchHotelByNameHandler : IRequestHandler<SearchHotelByName, IEnumerable<Hotel>>
    {
        private readonly ISearchPort _searchPort;

        public SearchHotelByNameHandler(ISearchPort searchPort)
        {
            _searchPort = searchPort;
        }

        public async Task<IEnumerable<Hotel>> Handle(SearchHotelByName request, CancellationToken cancellationToken)
        {
            return await _searchPort.SearchHotelByName(request.Name, request.Page);
        }
    }
}
