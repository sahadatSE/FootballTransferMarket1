using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    internal class PaymentMethod: BaseModel
    {
        [Key]
        public int PaymentMethodID { get; set; }
       
       
    }
}
