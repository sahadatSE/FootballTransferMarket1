using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public abstract class BaseModel  // ← Add 'abstract' keyword
    {
        public DateTime CreatedDate { get; set; }
        public string? CreatedBY { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBY { get; set; }
    }
}