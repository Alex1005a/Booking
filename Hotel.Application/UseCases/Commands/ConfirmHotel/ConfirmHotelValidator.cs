using FluentValidation;

namespace HotelSevice.Application.UseCases.Commands.ConfirmHotel
{
    public class ConfirmHotelValidator : AbstractValidator<ConfirmHotel>
    {
        public ConfirmHotelValidator()
        {
            RuleFor(s => s.HotelId)
                .NotEmpty();
        }
    }
}
