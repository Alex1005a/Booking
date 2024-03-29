﻿using FluentValidation;

namespace HotelSevice.Application.UseCases.Commands.UpdateHotelOwner
{
    public class UpdateHotelOwnerValidator : AbstractValidator<UpdateHotelOwner>
    {
        public UpdateHotelOwnerValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MinimumLength(5);
            RuleFor(s => s.PhoneNumber)
                .NotEmpty()
                .MinimumLength(500);
        }
    }
}
