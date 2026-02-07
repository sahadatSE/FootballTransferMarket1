using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    internal class PlayerBook : BaseModel
    {
        [Key]
        public string PlayerBookID { get; set; } = Guid.NewGuid().ToString();
        
        
    }
}
