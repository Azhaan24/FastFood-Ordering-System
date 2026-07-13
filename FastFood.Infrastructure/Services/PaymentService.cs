using FastFood.Core.DTOs.Payment;
using FastFood.Core.Entities;
using FastFood.Core.Interfaces;
using FastFood.Core.Settings;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace FastFood.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RazorpaySettings _settings;

    public PaymentService(
        IUnitOfWork unitOfWork,
        IOptions<RazorpaySettings> options)
    {
        _unitOfWork = unitOfWork;
        _settings = options.Value;
    }

    public async Task<PaymentResponseDto> CreatePaymentAsync(CreatePaymentDto dto)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(dto.OrderId);

        if (order == null)
            throw new Exception("Order not found.");

        RazorpayClient client =
            new RazorpayClient(_settings.Key, _settings.Secret);

        Dictionary<string, object> input = new()
        {
            { "amount", (int)(order.TotalAmount * 100) },
            { "currency", "INR" },
            { "receipt", $"Order_{order.Id}" }
        };

        Razorpay.Api.Order razorpayOrder =
            client.Order.Create(input);

        var payment = new Core.Entities.Payment
        {
            OrderId = order.Id,
            RazorpayOrderId = razorpayOrder["id"].ToString(),
            Amount = order.TotalAmount,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Payments.AddAsync(payment);

        await _unitOfWork.CompleteAsync();

        return new PaymentResponseDto
        {
            OrderId = order.Id.ToString(),
            RazorpayOrderId = payment.RazorpayOrderId,
            Amount = payment.Amount,
            Currency = "INR",
            Key = _settings.Key
        };
    }

    public async Task<bool> VerifyPaymentAsync(
        string razorpayOrderId,
        string razorpayPaymentId,
        string razorpaySignature)
    {
        string payload =
            razorpayOrderId + "|" + razorpayPaymentId;

        using var hmac =
            new HMACSHA256(
                Encoding.UTF8.GetBytes(_settings.Secret));

        var hash = hmac.ComputeHash(
            Encoding.UTF8.GetBytes(payload));

        string generated =
            BitConverter
            .ToString(hash)
            .Replace("-", "")
            .ToLower();

        if (generated != razorpaySignature)
            return false;

        var payment = (await _unitOfWork.Payments.FindAsync(
            p => p.RazorpayOrderId == razorpayOrderId))
            .FirstOrDefault();

        if (payment == null)
            return false;

        payment.RazorpayPaymentId = razorpayPaymentId;
        payment.Status = "Paid";

        _unitOfWork.Payments.Update(payment);

        var order = await _unitOfWork.Orders.GetByIdAsync(payment.OrderId);

        if (order != null)
        {
            order.Status = "Paid";
            _unitOfWork.Orders.Update(order);
        }

        await _unitOfWork.CompleteAsync();

        return true;
    }
}