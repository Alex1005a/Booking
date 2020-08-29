using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.UseCases.Commands.UpdateHotelStatus
{
    public class UpdateHotelStatusValidator : AbstractValidator<UpdateHotelStatus>
    {
        public UpdateHotelStatusValidator()
        {
            RuleFor(s => s.HotelId)
                .NotEmpty();
        }
    }
}
