using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class PlayerBookService(FTMContext context)
    {
        private readonly FTMContext _context = context;

        
        public async Task<Result> CheckAvailability(string playerId, DateTime bookDate, TimeSpan bookingDuration)
        {
            try
            {
                var exitDate = bookDate.Add(bookingDuration);
         
                var hasConflict = await _context.PlayerBook.AnyAsync(x =>
                    x.PlayerId == playerId &&
                    x.BookDate < exitDate &&          
                    x.ExitDate > bookDate);           

                if (hasConflict)
                {
                    return new Result(false, "Player is not available for the selected dates");
                }

                return new Result(true, "Player is available", new { IsAvailable = true });
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public async Task<Result> BookPlayer(PlayerBook playerBook)
        {
          
            if (!await _context.Player.AnyAsync(x => x.PlayerId == playerBook.PlayerId))
                return new Result(false, "Player does not exist");

            var exitDate = playerBook.BookDate.Add(playerBook.BookingDuration);

            var hasConflict = await _context.PlayerBook.AnyAsync(x =>
                x.PlayerId == playerBook.PlayerId &&
                x.BookDate < exitDate &&
                x.ExitDate > playerBook.BookDate);

            if (hasConflict)
            {
                return new Result(false, "Player is already booked for the selected dates");
            }

            var player = await _context.Player.FindAsync(playerBook.PlayerId);
            if (player != null)
            {
                playerBook.PlayerName = player.PlayerName;
            }

            await _context.PlayerBook.AddAsync(playerBook);
            return await Result.DBcommit(_context, "Player booked successfully", "Failed to book player", playerBook);
        }
        public async Task<Result> Update(PlayerBook playerBook)
        {
            if (!await _context.PlayerBook.AnyAsync(x => x.PlayerBookID == playerBook.PlayerBookID))
                return new Result(false, "Booking does not exist");

            var exitDate = playerBook.BookDate.Add(playerBook.BookingDuration);

            var hasConflict = await _context.PlayerBook.AnyAsync(x =>
                x.PlayerId == playerBook.PlayerId &&
                x.PlayerBookID != playerBook.PlayerBookID &&  
                x.BookDate < exitDate &&
                x.ExitDate > playerBook.BookDate);

            if (hasConflict)
            {
                return new Result(false, "Updated dates conflict with another booking");
            }

            _context.PlayerBook.Update(playerBook);
            return await Result.DBcommit(_context, "Booking updated successfully", "Failed to update booking", playerBook);
        }
        public async Task<Result> CancelBooking(string playerBookId)
        {
            if (!await _context.PlayerBook.AnyAsync(x => x.PlayerBookID == playerBookId))
                return new Result(false, "Booking does not exist");

            var booking = await _context.PlayerBook.FindAsync(playerBookId);

            if (booking == null)
                return new Result(false, "Booking does not exist");

            _context.PlayerBook.Remove(booking);
            return await Result.DBcommit(_context, "Booking cancelled successfully", "Failed to cancel booking");
        }

        public async Task<Result> List()
        {
            try
            {
                var bookings = await _context.PlayerBook
                    .OrderByDescending(x => x.BookDate)
                    .ToListAsync();

                return new Result(true, "Success", bookings);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        public async Task<Result> GetById(string playerBookId)
        {
            try
            {
                var booking = await _context.PlayerBook.FindAsync(playerBookId);

                if (booking == null)
                    return new Result(false, "Booking not found");

                return new Result(true, "Success", booking);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public async Task<Result> GetByPlayer(string playerId)
        {
            try
            {
                var bookings = await _context.PlayerBook
                    .Where(x => x.PlayerId == playerId)
                    .OrderByDescending(x => x.BookDate)
                    .ToListAsync();

                return new Result(true, "Success", bookings);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public async Task<Result> GetActiveBookings()
        {
            try
            {
                var now = DateTime.UtcNow;

                var bookings = await _context.PlayerBook
                    .Where(x => x.BookDate <= now && x.ExitDate > now)
                    .OrderBy(x => x.ExitDate)
                    .ToListAsync();

                return new Result(true, "Success", bookings);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public async Task<Result> GetUpcomingBookings()
        {
            try
            {
                var now = DateTime.UtcNow;

                var bookings = await _context.PlayerBook
                    .Where(x => x.BookDate > now)
                    .OrderBy(x => x.BookDate)
                    .ToListAsync();

                return new Result(true, "Success", bookings);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public async Task<Result> GetPastBookings()
        {
            try
            {
                var now = DateTime.UtcNow;

                var bookings = await _context.PlayerBook
                    .Where(x => x.ExitDate <= now)
                    .OrderByDescending(x => x.ExitDate)
                    .ToListAsync();

                return new Result(true, "Success", bookings);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public async Task<Result> GetAvailablePlayers(DateTime bookDate, TimeSpan bookingDuration)
        {
            try
            {
                var exitDate = bookDate.Add(bookingDuration);

                var bookedPlayerIds = await _context.PlayerBook
                    .Where(x => x.BookDate < exitDate && x.ExitDate > bookDate)
                    .Select(x => x.PlayerId)
                    .Distinct()
                    .ToListAsync();

                var availablePlayers = await _context.Player
                    .Where(x => !bookedPlayerIds.Contains(x.PlayerId))
                    .ToListAsync();

                return new Result(true, "Success", availablePlayers);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}