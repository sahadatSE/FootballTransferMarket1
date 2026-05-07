using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class TransferDetails : BaseModel
    {
        [Key]  
        public int TransferDetailsId { get;set;  }
       
        public string TransferID { get;set;  }=null!;
        [ForeignKey("TransferId")]
        public Transfer Transfer { get; set; }=null!;

        [Required]
        public decimal Amount { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}
