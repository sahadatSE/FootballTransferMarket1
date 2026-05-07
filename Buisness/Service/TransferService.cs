using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class TransferService(FTMContext context)
    {
        private readonly FTMContext _context = context;

        public async Task<Result> Add(Transfer transfer)
        {
            if (await _context.Transfer.AnyAsync(x =>
                x.FromClub == transfer.FromClub &&
                x.ToClub == transfer.ToClub &&
                x.TransferDate.Date == transfer.TransferDate.Date))
            {
                return new Result(false, "A transfer with these details already exists");
            }

            await _context.Transfer.AddAsync(transfer);
            return await Result.DBcommit(_context, "Transfer added successfully", "Failed to add transfer", transfer);
        }

        public async Task<Result> Update(Transfer transfer)
        {
            if (!await _context.Transfer.AnyAsync(x => x.TransferId == transfer.TransferId))
                return new Result(false, "Transfer does not exist");

            _context.Transfer.Update(transfer);
            return await Result.DBcommit(_context, "Transfer updated successfully", "Failed to update transfer", transfer);
        }

        public async Task<Result> Delete(Transfer transfer)
        {
            if (!await _context.Transfer.AnyAsync(x => x.TransferId == transfer.TransferId))
                return new Result(false, "Transfer does not exist");

            _context.Transfer.Remove(transfer);
            return await Result.DBcommit(_context, "Transfer deleted successfully", "Failed to delete transfer");
        }

        public async Task<Result> List()
        {
            try
            {
                var transfers = await _context.Transfer
                    .OrderByDescending(x => x.TransferDate)
                    .ToListAsync();

                return new Result(true, "Success", transfers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

    
        public async Task<Result> Single  (string transferId)
        {
            try
            {
                var transfer = await _context.Transfer.FindAsync(transferId);

                if (transfer == null)
                    return new Result(false, "Transfer not found");

                return new Result(true, "Success", transfer);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

       
        public async Task<Result> GetByFromClub(string fromClub)
        {
            try
            {
                var transfers = await _context.Transfer
                    .Where(x => x.FromClub == fromClub)
                    .OrderByDescending(x => x.TransferDate)
                    .ToListAsync();

                return new Result(true, "Success", transfers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        
        public async Task<Result> GetByToClub(string toClub)
        {
            try
            {
                var transfers = await _context.Transfer
                    .Where(x => x.ToClub == toClub)
                    .OrderByDescending(x => x.TransferDate)
                    .ToListAsync();

                return new Result(true, "Success", transfers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

       
        public async Task<Result> GetByClub(string clubName)
        {
            try
            {
                var transfers = await _context.Transfer
                    .Where(x => x.FromClub == clubName || x.ToClub == clubName)
                    .OrderByDescending(x => x.TransferDate)
                    .ToListAsync();

                return new Result(true, "Success", transfers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

      
        public async Task<Result> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var transfers = await _context.Transfer
                    .Where(x => x.TransferDate >= startDate && x.TransferDate <= endDate)
                    .OrderByDescending(x => x.TransferDate)
                    .ToListAsync();

                return new Result(true, "Success", transfers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

       
        public async Task<Result> GetRecent()
        {
            try
            {
                var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

                var transfers = await _context.Transfer
                    .Where(x => x.TransferDate >= thirtyDaysAgo)
                    .OrderByDescending(x => x.TransferDate)
                    .ToListAsync();

                return new Result(true, "Success", transfers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetByYear(int year)
        {
            try
            {
                var transfers = await _context.Transfer
                    .Where(x => x.TransferDate.Year == year)
                    .OrderByDescending(x => x.TransferDate)
                    .ToListAsync();

                return new Result(true, "Success", transfers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetHighValueTransfers(decimal minFee)
        {
            try
            {
                var transfers = await _context.Transfer
                    .Where(x => decimal.Parse(x.TransferFee ?? "0") >= minFee)
                    .OrderByDescending(x => decimal.Parse(x.TransferFee ?? "0"))
                    .ToListAsync();

                return new Result(true, "Success", transfers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetStatistics()
        {
            try
            {
                var allTransfers = await _context.Transfer.ToListAsync();

                if (allTransfers.Count == 0)
                    return new Result(true, "No transfers found", new { TotalTransfers = 0 });

                var stats = new
                {
                    TotalTransfers = allTransfers.Count,
                    TotalValue = allTransfers.Sum(x => decimal.Parse(x.TransferFee ?? "0")),
                    AverageValue = allTransfers.Average(x => decimal.Parse(x.TransferFee ?? "0")),
                    HighestValue = allTransfers.Max(x => decimal.Parse(x.TransferFee ?? "0")),
                    LowestValue = allTransfers.Min(x => decimal.Parse(x.TransferFee ?? "0")),
                    MostActiveClub = allTransfers
                        .SelectMany(t => new[] { t.FromClub, t.ToClub })
                        .GroupBy(c => c)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault()?.Key
                };

                return new Result(true, "Success", stats);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}