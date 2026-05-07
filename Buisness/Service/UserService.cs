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
            await _context.UserInfo.AddAsync(user);
            return await Result.DBcommit(_context, "User created successfully", "Failed to create user");
        }
        public async Task<Result> Update(UserInfo user)
        {
            return await Result.DBcommit(_context, "Update Succesesfully!", null, user);
        }
        public async Task<Result> List()
        {
            try
            {
                var Users = _context.UserInfo.ToList();
                return  new Result(true, "Success", Users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }

        }
        public async Task<Result>single (string Id )
        {
            try
            {
                var User = _context.UserInfo.Where(x => x.UserInfoId == Id).FirstOrDefault();
                return new Result(true, "Success", User);
            }
            catch(Exception ex)
            {
                return new Result(false,ex.Message);
            }
        }
    }
}
