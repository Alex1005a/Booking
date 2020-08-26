﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.UseCases.Commands.UpdateHotelOwner
{
    class UpdateHotelOwnerValidator : AbstractValidator<UpdateHotelOwner>
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