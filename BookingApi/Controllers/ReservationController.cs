using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        public void Test()
        {
            Console.WriteLine(1);
        }
    }
}
