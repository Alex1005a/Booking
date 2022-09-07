using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        public AccountController()
        {
            
        }

        [HttpGet]
        public Task<string> Get()
        {
            return Task.FromResult("AccountController");
        }
    }
}