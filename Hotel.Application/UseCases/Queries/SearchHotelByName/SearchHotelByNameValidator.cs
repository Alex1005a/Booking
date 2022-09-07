using FluentValidation;

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
