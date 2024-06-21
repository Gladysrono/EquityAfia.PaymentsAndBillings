﻿// API/Controllers/BillingAndPaymentController.cs
using EquityAfia.PaymentsAndBillings.Application.Interfaces;
using EquityAfia.PaymentsAndBillings.Contracts.Billing;
using EquityAfia.PaymentsAndBillings.Contracts.Payment.Card;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BillingAndPaymentController : ControllerBase
{
    private readonly IBillingService _billingService;
    private readonly IPaymentService _paymentService;
    private readonly IStripeService _stripeService;

    public BillingAndPaymentController(IBillingService billingService, IPaymentService paymentService, IStripeService stripeService)
    {
        _billingService = billingService;
        _paymentService = paymentService;
        _stripeService = stripeService;
    }

    [HttpPost("billing")]
    public async Task<IActionResult> AddBillingWithServices([FromBody] BillingDto billingDto)
    {
        if (billingDto == null)
            return BadRequest("Billing data is required");

        var result = await _billingService.AddBillingWithServicesAsync(billingDto);
        return Ok(result);
    }

    [HttpPost("payment/confirm")]
    public async Task<IActionResult> ConfirmPayment([FromBody] PaymentConfirmationRequest request)
    {
        if (request == null)
            return BadRequest("Request data is required");

        try
        {
            await _paymentService.ConfirmPaymentAsync(request);
            return Ok(new { message = "Payment confirmed successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("stripe/create-payment-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreateStripePaymentIntentRequest request)
    {
        if (request == null)
            return BadRequest("Request data is required");

        try
        {
            var paymentIntent = await _stripeService.CreatePaymentIntent(request.Amount, request.Currency, request.PaymentMethod, request.BillingId, request.CustomerId, request.CustomerName, request.CustomerEmail);
            return Ok(new { paymentIntentId = paymentIntent.Id, clientSecret = paymentIntent.ClientSecret });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("stripe/confirm-payment-intent")]
    public async Task<IActionResult> ConfirmPaymentIntent([FromBody] ConfirmStripePaymentIntentRequest request)
    {
        if (request == null)
            return BadRequest("Request data is required");

        try
        {
            var paymentIntent = await _stripeService.ConfirmPaymentIntent(request.PaymentIntentId, request.PaymentMethod);
            return Ok(new { paymentIntentId = paymentIntent.Id, status = paymentIntent.Status });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}

//
