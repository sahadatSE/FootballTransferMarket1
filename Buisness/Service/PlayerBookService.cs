using Database.Context;
using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SlotBookService
    {
       FootballTransferMarket footballTransferMarket = new FootballTransferMarket();

        
        public Result Book(PlayerBook playerBook)
        {
            //?? Payment...
            footballTransferMarket.PlayerBook.Add(playerBook );
            var slot = footballTransferMarket.Player.FirstOrDefault(x => x.PlayerId == slotBook.PlayerId);
            slot.IsBooked = true;
            footballTransferMarket.Slot.Update(slot);
            return new Result().DBCommit(footballTransferMarket,"Booked Successfully!", null,playerBook);
        }
    }
}