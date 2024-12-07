using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    internal class OfferForSale:Player 
    {
        [Key]
        public string MarketId { get; set; } = Guid.NewGuid().ToString();
    }
}
