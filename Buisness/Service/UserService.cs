using Database.Context;
using Database.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Service
{
    public class UserService(FTMContext context)
    {
        private readonly FTMContext _context = context;
        public async Task<Result> CreateUser(UserInfo user)
        {
   
            if (await _context.UserInfo.AnyAsync(x => x.UserEmail == user.UserEmail))
                return new Result(false, "User with this email already exists");

            await _context.UserInfo.AddAsync(user);
            return await Result.DBcommit(_context, "User created successfully", "Failed to create user");
        }

        public async Task<Result> Update(UserInfo user)
        {
            if (!await _context.UserInfo.AnyAsync(x => x.UserInfoId == user.UserInfoId))
                return new Result(false, "User does not exist");

            _context.UserInfo.Update(user);
            return await Result.DBcommit(_context, "Update Successfully!", null, user);
        }
        public async Task<Result> Delete(UserInfo User)
        {
            if (!await _context.UserInfo.AnyAsync(x => x.UserInfoId == User.UserInfoId))
                return new Result(false, "User does not exist");

            _context.UserInfo.Remove(User);
            return await Result.DBcommit(_context, "Deleted successfully", "Failed to delete user");
        }
        public async Task<Result> List()
        {
            try
            {
                var users = await _context.UserInfo.ToListAsync();
                return new Result(true, "Success", users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public async Task<Result> Single(string id)
        {
            try
            {
                var user = await _context.UserInfo.FindAsync(id);
                if (user == null)
                    return new Result(false, "User not found");

                return new Result(true, "Success", user);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
       
    }
}