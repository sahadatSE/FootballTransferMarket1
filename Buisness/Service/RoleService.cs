using System.Data;
using Database.Context;
using Database.Model;

namespace Business.Services
{
    public class RoleService
    {
        FootballTransferMarket footballTransferMarket = new FootballTransferMarket();
        public Result Add(Role role)
        {
            if (footballTransferMarket.Role.Any(x => x.Name == role.Name))
                return new Result(false, "Role already exist!");
            footballTransferMarket.Role.Add(role);
            return new Result().DBCommit(footballTransferMarket, "Save Successfully!", null, role);
        }
        public Result Update(Role role)
        {
            if (!footballTransferMarket.Role.Any(x => x.RoleId == role.RoleId))
                return new Result(false, "Role not exist!");
            footballTransferMarket.Role.Update(role);
            return new Result().DBCommit(footballTransferMarket, "Updated Successfully!", null, role);
        }
        public Result List()
        {
            try
            {
                var roles = footballTransferMarket.Role.ToList();
                return new Result(true, "Success", roles);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
        public Result Single(int Id)
        {
            try
            {
                var role = footballTransferMarket.Role.Where(x => x.RoleId == Id).FirstOrDefault();
                return new Result(true, "Success", role);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
