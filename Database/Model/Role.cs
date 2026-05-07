using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Role:BaseModel
    {
        [Key]
        public int RoleID { get; set; }
        public string Name { get; set; }= null!;    
        public bool IsActive { get; set; }
    }
}
