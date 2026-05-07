using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class PlayerBook : BaseModel
    {
        [Key]
        [Required]
        public string PlayerBookID { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public DateTime BookDate { get; set; } = DateTime.UtcNow;
        [Required]
        public TimeSpan BookingDuration { get; set; } = TimeSpan.FromDays(1);

        public DateTime ExitDate => BookDate.Add(BookingDuration);

        public string ExitDateFormatted => ExitDate.ToString("dd-MM-yyyy");

        public string PlayerId { get; set; } = null!;
        [ForeignKey("PlayerId")]
        public Player Player { get; set; } = null!;
        public string PlayerName { get; set; } = null!;

    }
}
