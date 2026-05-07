using System;
using System.Collections.Generic;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class TransferDetailsService(FTMContext context)
    {
        private readonly FTMContext _context = context;

        public async Task<Result> Add(TransferDetails detail)
        {
            if (!await _context.Transfer.AnyAsync(x => x.TransferId == detail.TransferID))
                return new Result(false, "Transfer does not exist");

            await _context.TransferDetails.AddAsync(detail);
            return await Result.DBcommit(_context, "Added successfully", "Failed to add", detail);
        }

        public async Task<Result> Update(TransferDetails detail)
        {
            if (!await _context.TransferDetails.AnyAsync(x => x.TransferDetailsId == detail.TransferDetailsId))
                return new Result(false, "Detail does not exist");

            _context.TransferDetails.Update(detail);
            return await Result.DBcommit(_context, "Updated successfully", "Failed to update", detail);
        }

        public async Task<Result> Delete(int detailId)
        {
            if (!await _context.TransferDetails.AnyAsync(x => x.TransferDetailsId == detailId))
                return new Result(false, "Detail does not exist");

            var detail = await _context.TransferDetails.FindAsync(detailId);

            if (detail == null)
                return new Result(false, "Detail does not exist");

            _context.TransferDetails.Remove(detail);
            return await Result.DBcommit(_context, "Deleted successfully", "Failed to delete");
        }

        public async Task<Result> List()
        {
            try
            {
                var details = await _context.TransferDetails.ToListAsync();
                return new Result(true, "Success", details);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetById(int detailId)
        {
            try
            {
                var detail = await _context.TransferDetails.FindAsync(detailId);

                if (detail == null)
                    return new Result(false, "Not found");

                return new Result(true, "Success", detail);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetByTransfer(string transferId)
        {
            try
            {
                var details = await _context.TransferDetails
                    .Where(x => x.TransferID == transferId)
                    .ToListAsync();

                return new Result(true, "Success", details);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}