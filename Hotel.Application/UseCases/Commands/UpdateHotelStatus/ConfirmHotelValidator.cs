using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.UseCases.Commands.UpdateHotelStatus
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
