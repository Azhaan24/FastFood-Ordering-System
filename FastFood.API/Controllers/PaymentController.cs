using FastFood.Core.DTOs.Payment;
using FastFood.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace FastFood.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> Create(CreatePaymentDto dto)
    {
        return Ok(await _paymentService.CreatePaymentAsync(dto));
    }

    [HttpPost("verify")]
    public async Task<IActionResult> Verify(
        string razorpayOrderId,
        string razorpayPaymentId,
        string razorpaySignature)
    {
        var result = await _paymentService.VerifyPaymentAsync(
            razorpayOrderId,
            razorpayPaymentId,
            razorpaySignature);

        if (!result)
            return BadRequest();

        return Ok(new
        {
            Message = "Payment Successful"
        });
    }
}