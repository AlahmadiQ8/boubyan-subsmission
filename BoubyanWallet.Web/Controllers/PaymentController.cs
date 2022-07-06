using BoubyanWallet.Web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BoubyanWallet.Web.Controllers;

[Authorize]
[ApiController]
[Route("/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly UserManager<Customer> _userManager;

    public PaymentController(IPaymentService paymentService, UserManager<Customer> userManager)
    {
        _paymentService = paymentService;
        _userManager = userManager;
    }

    [HttpPost("/submit")]
    public async Task<IActionResult> SubmitPayment([FromBody] Payment payment)
    {
        payment.Payer = await _userManager.GetUserAsync(HttpContext.User);
        if (payment.PaymentType == PaymentType.Tranfer)
        {
            await _paymentService.SubmitPaymentTransfer(payment);
        }
        else if (payment.PaymentType == PaymentType.Payment)
        {
            await _paymentService.SubmitPaymentRequest(payment);
        }

        return NoContent();
    }
}