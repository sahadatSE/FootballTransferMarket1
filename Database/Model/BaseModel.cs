using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public  class BaseModel  
    {
        [Key]
        public int  Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBY { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBY { get; set; }
    }
}