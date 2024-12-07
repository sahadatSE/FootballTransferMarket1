
using System.ComponentModel.DataAnnotations;
namespace Database.Model
{
    public  class Player
    {
        [Key]
        public string PlayerId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string? PlayerName { get; set; }
        [Required]
        public string? Position { get; set; }
        [Required]
        public string? PlayerAge { get; set; }
        [Required]
        public string? Nationality { get; set; }
        [Required]
        public string? CurrentClub { get; set; }
        [Required]
        public string? MarketValue { get; set; }
        [Required]
        public string? ContractExpiry { get; set; }
        [Required]
        public int? Rating { get; set; }
        public bool IsActive { get; set; }
    }
}
