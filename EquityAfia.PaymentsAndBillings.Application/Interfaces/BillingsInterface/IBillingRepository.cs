﻿using EquityAfia.PaymentsAndBillings.Contracts.Billing;
using EquityAfia.PaymentsAndBillings.Domain.Entities;
using System.Threading.Tasks;

namespace EquityAfia.PaymentsAndBillings.Application.Interfaces.Billing
{
    public interface IBillingRepository
    {
        Task<IBillingRepository> AddAsync(IBillingRepository billing);
        Task GetBillingByIdAsync(int billingId);
    }
}
