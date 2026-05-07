using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class Payment: BaseModel
    {
        [Key]
        [Required]
        public string PaymentId { get; set; } = Guid.NewGuid().ToString(); 
        public string PlayerBookId { get; set; } = null!;   
        public double  Amount { get; set; }
        public int PaymentMethodId { get; set; }



    }
}

