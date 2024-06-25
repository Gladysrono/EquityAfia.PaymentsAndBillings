﻿namespace EquityAfia.PaymentsAndBillings.Application.Interfaces.Billing
{
    public interface IBillingRepository
    {
        Task AddAsync(IBillingRepository);
        Task<IBillingRepository> GetBillingByIdOrAppointmentIdAsync(int billingId, int appointmentId);
        Task<IBillingRepository> GetBillingByIdAsync(int billingId);
    }
}
