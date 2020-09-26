using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.UseCases.Queries.SearchHotelByName
{
    public class SearchHotelByNameValidator : AbstractValidator<SearchHotelByName>
    {
        public SearchHotelByNameValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MinimumLength(5);
            RuleFor(s => s.Page)
                .GreaterThan(-1);
        }
    }
}
