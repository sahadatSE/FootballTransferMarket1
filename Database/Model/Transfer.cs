

using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Transfer:Player 
    {
        [Key]
        public string TransferId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string? FromClub { get; set; }
        [Required]
        public string? ToClub { get; set; }
        [Required]
        public string? TransferFee { get; set; }
        [Required]
        public string? TransferDate { get; set; }
    }
}
