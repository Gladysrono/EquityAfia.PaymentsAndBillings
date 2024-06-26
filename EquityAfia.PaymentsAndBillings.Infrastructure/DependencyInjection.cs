﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EquityAfia.PaymentsAndBillings.Infrastructure.Data;
using EquityAfia.PaymentsAndBillings.Application.Interfaces;
using EquityAfia.PaymentsAndBillings.Application.Services;
using EquityAfia.PaymentsAndBillings.Application.Interfaces.Billing;
using EquityAfia.PaymentsAndBillings.Application.Services.BillingService;
using EquityAfia.PaymentsAndBillings.Application.Services.PaymentService;
using EquityAfia.PaymentsAndBillings.Application.Services.PaymentService.StripeService;
using EquityAfia.PaymentsAndBillings.Application.Interfaces.Payments.Stk;
using EquityAfia.PaymentsAndBillings.Application.Services.PaymentService.StkFolder;
using EquityAfia.PaymentsAndBillings.Infrastructure.Repositories;
using EquityAfia.PaymentsAndBillings.Application.Repositories;

namespace EquityAfia.PaymentsAndBillings.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<EquityAfiaDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            services.AddScoped<IBillingRepository, BillingRepository>();

            // Register services
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEPharmacyService, EPharmacyService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IStripeService, StripeService>();
            services.AddScoped<IStkService, StkService>();

            // Register AutoMapper with fully qualified method call
            ServiceCollec.AddAutoMapper(
                services,
                typeof(EquityAfia.PaymentsAndBillings.Application.Mappings.MappingProfile).Assembly
            );

            return services;
        }
    }
}
