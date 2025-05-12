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
        [Required]
        pulic int PlayerId { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatIDEditMode = true, DateFormatString = "0:yyyy-MM-ddTHH:mm")]
        public DateTime BookingTime {get;set;}
        [Required]
        public int BookingDuration { get; set; }
        [Required]
        public string? RegNo { get; set; }
        public bool IsExitRequested { get; set; }
        public DateTime ProbableExitTime { get; set; }

    }
}
