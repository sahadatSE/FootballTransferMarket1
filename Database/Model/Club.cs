
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Club:BaseModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string ClubName { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        public string? League { get; set; }

        public string? Stadium { get; set; }

        public string? Logo { get; set; }

        public decimal TransferBudget { get; set; }

        public string? Description { get; set; }

        public string? ContactEmail { get; set; }

        public bool IsVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
