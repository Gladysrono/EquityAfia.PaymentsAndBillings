﻿using EquityAfia.PaymentsAndBillings.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityAfia.PaymentsAndBillings.Application.Interfaces.Payments.Stk
{
    public interface  IStkService
    {
        Task<Payment> MakeStkPaymentAsync(int billingId, string mobileNumber);
    }
    _configuration = configuration;
    _context = context;
}
public async Task<Payment> MakeStkPaymentAsync(int billingId, string mobileNumber)
{
    var billing = await _context.Billings.FindAsync(billingId);
    if (billing == null) throw new Exception("Billing not found");
}
