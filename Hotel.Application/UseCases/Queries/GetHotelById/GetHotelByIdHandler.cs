using Hotel.Domain.AggregatesModel.HotelAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.UseCases.Queries.GetHotelById
{
    public class GetHotelByIdHandler : IRequestHandler<GetHotelById, HotelAggregate>
    {
        private readonly ISearchPort _searchPort;

        public GetHotelByIdHandler(ISearchPort searchPort)
        {
            _searchPort = searchPort;
        }
        public async Task<HotelAggregate> Handle(GetHotelById request, CancellationToken cancellationToken)
        {      
            return await _searchPort.GetByIdAsync(request.Id);
        }
    }
}
