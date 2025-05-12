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
        public Result Registration(UserForm user)
        {
            bool x = footballTransferMarketContext.UserAny(x => x.Email == user.Email);
            if (x) return new Result(false, "Email already registered!");
            UserInfo userInfo = new UserInfo();
            userInfo.FullName = user.FullName;
            userInfo.Email = user.Email;
            userInfo.PasswordHash = new PasswordHasher<UserInfo>().HashPassword(userInfo, user.Password);
            userInfo.RoleId = user.RoleId == 0 ? 3 : user.RoleId;
            userInfo.IsActive = true;
            userInfo.CreatedBy = user.CreatedBy;
            userInfo.UpdatedBy = user.UpdatedBy;
            userInfo.UpdatedDate = user.UpdatedDate;
            carParkingContext.UserInfo.Add(userInfo);
            return new Result().DBCommit(footballTransferMarketContext, "Registered Successfully!", null, user);

        }
        public Result Login(UserLoginForm user)
        {
            UserInfo? user = footballTransferMarketContext.User.Where(x => x.Email == user.Email).FirstOrDefault();
            if (user == null) return new Result(false, "Register First!");

            PasswordVerificationResult HashResult = new PasswordHasher<UserInfo>().VerifyHashedPassword(userInfo, userInfo.PasswordHash, user.Password);
            if (HashResult != PasswordVerificationResult.Failed)
            {
                return new Result(true, $"{user.FullName} successfully logged in!", user);
            }
            else
            {
                return new Result(false, "Incorrect Password");
            }
        }

        public Result Update(UserForm user)
        {
            //logics
            return new Result().DBCommit(footballTransferMarketContext, "Updated Successfully!", null, user);
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
                var User = footballTransferMarketContext.User.Where(x => x.UserId == Id).FirstOrDefault();
                return new Result(true, "Success", User);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
