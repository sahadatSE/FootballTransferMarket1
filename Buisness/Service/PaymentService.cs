using System;
using System.Collections.Generic;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class PaymentService(FTMContext context)
    {
        private readonly FTMContext _context = context;

        public async Task<Result> Add(Payment payment)
        {
            if (!await _context.PlayerBook.AnyAsync(x => x.PlayerBookID == payment.PlayerBookId))
                return new Result(false, "Player booking does not exist");

            if (!await _context.PaymentMethod.AnyAsync(x => x.PaymentMethodID == payment.PaymentMethodId))
                return new Result(false, "Payment method does not exist");

            if (payment.Amount <= 0)
                return new Result(false, "Amount must be greater than zero");

            await _context.Payment.AddAsync(payment);
            return await Result.DBcommit(_context, "Payment added successfully", "Failed to add payment", payment);
        }

        public async Task<Result> Update(Payment payment)
        {
            if (!await _context.Payment.AnyAsync(x => x.PaymentId == payment.PaymentId))
                return new Result(false, "Payment does not exist");

            if (payment.Amount <= 0)
                return new Result(false, "Amount must be greater than zero");

            _context.Payment.Update(payment);
            return await Result.DBcommit(_context, "Payment updated successfully", "Failed to update payment", payment);
        }

        public async Task<Result> Delete(string paymentId)
        {
            if (!await _context.Payment.AnyAsync(x => x.PaymentId == paymentId))
                return new Result(false, "Payment does not exist");

            var payment = await _context.Payment.FindAsync(paymentId);

            if (payment == null)
                return new Result(false, "Payment does not exist");

            _context.Payment.Remove(payment);
            return await Result.DBcommit(_context, "Payment deleted successfully", "Failed to delete payment");
        }

        public async Task<Result> List()
        {
            try
            {
                var payments = await _context.Payment.ToListAsync();
                return new Result(true, "Success", payments);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetById(string paymentId)
        {
            try
            {
                var payment = await _context.Payment.FindAsync(paymentId);

                if (payment == null)
                    return new Result(false, "Payment not found");

                return new Result(true, "Success", payment);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetByPlayerBook(string playerBookId)
        {
            try
            {
                var payments = await _context.Payment
                    .Where(x => x.PlayerBookId == playerBookId)
                    .ToListAsync();

                return new Result(true, "Success", payments);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetByPaymentMethod(int paymentMethodId)
        {
            try
            {
                var payments = await _context.Payment
                    .Where(x => x.PaymentMethodId == paymentMethodId)
                    .ToListAsync();

                return new Result(true, "Success", payments);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetTotalAmount(string playerBookId)
        {
            try
            {
                var total = await _context.Payment
                    .Where(x => x.PlayerBookId == playerBookId)
                    .SumAsync(x => x.Amount);

                return new Result(true, "Success", new { PlayerBookId = playerBookId, TotalAmount = total });
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}