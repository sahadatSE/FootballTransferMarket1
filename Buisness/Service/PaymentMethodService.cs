using System;
using System.Collections.Generic;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class PaymentMethodService(FTMContext context)
    {
        private readonly FTMContext _context = context;
        public async Task<Result> Add(PaymentMethod paymentMethod)
        {
            if (await _context.PaymentMethod.AnyAsync(x => x.PaymentMethodName == paymentMethod.PaymentMethodName))
                return new Result(false, "Payment method already exists");

            await _context.PaymentMethod.AddAsync(paymentMethod);
            return await Result.DBcommit(_context, "Payment method added successfully", "Failed to add payment method", paymentMethod);
        }

        public async Task<Result> Update(PaymentMethod paymentMethod)
        {
            if (!await _context.PaymentMethod.AnyAsync(x => x.PaymentMethodID == paymentMethod.PaymentMethodID))
                return new Result(false, "Payment method does not exist");

            _context.PaymentMethod.Update(paymentMethod);
            return await Result.DBcommit(_context, "Payment method updated successfully", "Failed to update payment method", paymentMethod);
        }

        public async Task<Result> Delete(int paymentMethodId)
        {
            if (!await _context.PaymentMethod.AnyAsync(x => x.PaymentMethodID == paymentMethodId))
                return new Result(false, "Payment method does not exist");

            var paymentMethod = await _context.PaymentMethod.FindAsync(paymentMethodId);

            if (paymentMethod == null)
                return new Result(false, "Payment method does not exist");

            _context.PaymentMethod.Remove(paymentMethod);
            return await Result.DBcommit(_context, "Payment method deleted successfully", "Failed to delete payment method");
        }

        public async Task<Result> List()
        {
            try
            {
                var paymentMethods = await _context.PaymentMethod.ToListAsync();
                return new Result(true, "Success", paymentMethods);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetById(int paymentMethodId)
        {
            try
            {
                var paymentMethod = await _context.PaymentMethod.FindAsync(paymentMethodId);

                if (paymentMethod == null)
                    return new Result(false, "Payment method not found");

                return new Result(true, "Success", paymentMethod);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetActive()
        {
            try
            {
                var paymentMethods = await _context.PaymentMethod
                    .Where(x => x.IsActive == true)
                    .ToListAsync();

                return new Result(true, "Success", paymentMethods);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> Activate(int paymentMethodId)
        {
            var paymentMethod = await _context.PaymentMethod.FindAsync(paymentMethodId);

            if (paymentMethod == null)
                return new Result(false, "Payment method does not exist");

            paymentMethod.IsActive = true;
            return await Result.DBcommit(_context, "Payment method activated", "Failed to activate");
        }

        public async Task<Result> Deactivate(int paymentMethodId)
        {
            var paymentMethod = await _context.PaymentMethod.FindAsync(paymentMethodId);

            if (paymentMethod == null)
                return new Result(false, "Payment method does not exist");

            paymentMethod.IsActive = false;
            return await Result.DBcommit(_context, "Payment method deactivated", "Failed to deactivate");
        }
    }
}