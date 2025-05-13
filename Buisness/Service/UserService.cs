using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.FormModel;
using Database.Context;
using Microsoft.AspNetCore.Identity;

namespace Buisness.Service
{
    internal class UserService
    {
        FootballTransferMarketContext footballTransferMarketContext = new FootballTransferMarketContext();
        public Result Registration(UserForm users)
        {
            bool x = footballTransferMarketContext.UserAny(x => x.Email == user.Email);
            if (x) return new Result(false, "Email already registered!");
            User user = new User();
            user.FullName = users.FullName;
            user.Email = users.Email;
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, users.Password);
            user.RoleId = users.RoleId == 0 ? 3 : users.RoleId;
            user.IsActive = true;
            user.CreatedBy = users.CreatedBy;
            user.UpdatedBy = users.UpdatedBy;
            user.UpdatedDate = users.UpdatedDate;
            carParkingContext.User.Add(user);
            return new Result().DBCommit(footballTransferMarketContext, "Registered Successfully!", null, users);

        }
        public Result Login(UserLoginForm users)
        {
            User? user = footballTransferMarketContext.User.Where(x => x.Email == users.Email).FirstOrDefault();
            if (user == null) return new Result(false, "Register First!");

            PasswordVerificationResult HashResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, users.Password);
            if (HashResult != PasswordVerificationResult.Failed)
            {
                return new Result(true, $"{user.FullName} successfully logged in!", user);
            }
            else
            {
                return new Result(false, "Incorrect Password");
            }
        }

        public Result Update(UserForm users)
        {
            //logics
            return new Result().DBCommit(footballTransferMarketContext, "Updated Successfully!", null, users);
        }
        public Result List()
        {
            //logics
            try
            {
                var Users = footballTransferMarketContext.User.ToList();
                return new Result(true, "Success", Users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(string Id)
        {
            //logics
            try
            {
                var Users = footballTransferMarketContext.User.Where(x => x.UserId == Id).FirstOrDefault();
                return new Result(true, "Success", Users);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
