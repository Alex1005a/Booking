using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelSevice.Infrastructure.Services
{
    public interface IIdempotencyService
    {
        string GetKey(string key);
        void SetKey(string key, string result);
    }
}
