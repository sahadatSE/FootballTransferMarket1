using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    internal class Role:BaseModel
    {
        [Key]
        public int RoleID { get; set; }
        public int Name { get; set; }
        public int IsActive { get; set; }
    }
}
