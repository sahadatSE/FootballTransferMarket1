using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    internal class Payment: BaseModel
    {
        public string PamentID { get; set} = Guid.NewGuid().ToString();
        public string? TransferId { get; set; }
        public double Amount { get; set; }
        public int paymentMethodID { get; set; }
    }
}
