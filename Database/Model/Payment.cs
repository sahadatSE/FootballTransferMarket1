using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class Payment: BaseModel
    {
        public string PaymentId { get; set; } = Guid.NewGuid().ToString(); 
        public decimal Amount { get; set; }
       
    }
}

