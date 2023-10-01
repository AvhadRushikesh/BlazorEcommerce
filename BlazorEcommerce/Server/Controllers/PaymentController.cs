using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("checkout"), Authorize]
        public async Task<ActionResult<string>> CreateCheckoutSession()
        {
            var session = await _paymentService.CreateCheckoutSession();
            return Ok(session.Url);
        }

        /* stripe  listen --forward-to https://localhost:7283/api/payment 
         * Run the above command in terminal to hit this method after making payment
         * Will remove order from Cart & add order to database
         */
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> FulFillOrder()
        {
            var response = await _paymentService.FulfillOrder(Request);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
    }
}
