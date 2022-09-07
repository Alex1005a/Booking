using FluentValidation;

namespace HotelSevice.Application.UseCases.Commands.CreateHotel
{
    public class CreateHotelValidator : AbstractValidator<CreateHotel>
    {
        public CreateHotelValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MinimumLength(5);
            RuleFor(s => s.Description)
                .NotEmpty()
                .MaximumLength(500);
        }

    }
}
