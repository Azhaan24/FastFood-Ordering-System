using FastFood.Core.DTOs.Payment;

namespace FastFood.Core.Interfaces;

public interface IPaymentService
{
    Task<PaymentResponseDto> CreatePaymentAsync(CreatePaymentDto dto);

    Task<bool> VerifyPaymentAsync(
        string razorpayOrderId,
        string razorpayPaymentId,
        string razorpaySignature);
}