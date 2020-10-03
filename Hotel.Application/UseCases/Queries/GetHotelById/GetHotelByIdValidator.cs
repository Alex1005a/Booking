﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Application.UseCases.Queries.GetHotelById
{
    public class GetHotelByIdValidator : AbstractValidator<GetHotelById>
    {
        public GetHotelByIdValidator()
        {
            RuleFor(s => s.Id)
                .NotEmpty()
                .Must(id => !id.Contains(" "));
        }
    }
}
