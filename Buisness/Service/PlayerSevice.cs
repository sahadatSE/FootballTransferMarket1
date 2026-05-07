using System;
using System.Collections.Generic;
using System.Text;
using Database.Context;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class PlayerSevice(FTMContext context)
    {
        private readonly FTMContext _context = context;
        public async Task<Result> Add(Player player)
        {
            if (await _context.Player.AnyAsync(x => x.PlayerName == player.PlayerName))
                return new Result(false, "Player already exits");

            _context.Player.Add(player);
            return await Result.DBcommit(_context,"Save Successfully", null, player);
        }
        public async Task<Result> Update(Player player)
        {
           
            if (!await _context.Player.AnyAsync(x => x.PlayerId == player.PlayerId))
                return new Result(false, "Player does not exist");

            _context.Player.Update(player);
            return await Result.DBcommit(_context, "Update Successfully", null, player);
        }
        public async Task<Result> Delete(Player player)
        {
            if (!await _context.Player.AnyAsync(x => x.PlayerId == player.PlayerId))
                return new Result(false, "Player does not exist");

            _context.Player.Remove(player);
            return await Result.DBcommit(_context, "Deleted successfully", "Failed to delete player");
        }
        public async Task<Result>List()
        {
            try
            {
                var Palyers = await _context.Player.ToListAsync();
                return new Result(true, "Success", Palyers);
            }
            catch (Exception ex )
            {
                return new Result(false, ex.Message);
            }
        }

    }
}
