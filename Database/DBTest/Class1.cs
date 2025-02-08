using Database.Context;
using Database.Model;
using System.Data;

namespace Database.DBTest
{
    internal class Class1
    {
        static void Main(string[] args)
        {
            FootballTransferMarket db = new FootballTransferMarket();

            var AllUser = db.UserRoleInfo.ToList();



        }
    }
}
