using KASHOP.BLL.Service;
using KASHOP.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace KASHOP.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutsController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Payment([FromBody] CheckoutRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _checkoutService.ProcessPaymentAsync(request, userId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("success")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromQuery] string session_id)
        {
            /*var service = new SessionService();
            var session = service.Get(session_id);
            var userId = session.Metadata["UserId"];

            return Ok(new
            {
                Message = "success",
                UserId = userId,
            });
            */

            var response = await _checkoutService.HandleSuccessAsync(session_id);

            //var service = new SessionService();
            //var session = service.Get(session_id);
            //var userId = session.Metadata["userId"];

            if (!response.Success) return BadRequest(response);

            return Ok(response);

        }



    }
}
