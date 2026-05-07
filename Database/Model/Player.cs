
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
namespace Database.Model
{
    public  class Player:BaseModel
    {
        [Key]
        public string PlayerId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(100)]
        public string PlayerName { get; set; }= null!;      
        [Required]
        [StringLength(50)]
        public string Position { get; set; } = null!;    
        [Required]
        [StringLength(3)]
        public string PlayerAge { get; set; }=  null!;
        [Required]
        [StringLength(100)]
        public string Nationality { get; set; }=null!;  
        [Required]
        [StringLength(100)] 
        public string CurrentClub { get; set; }= null!;
        [Required]
        public String PreviousClub { get; set; } = null!;
        [Required]
        public decimal MarketValue { get; set; }
        [Required]
        public DateTime ContractExpiry { get; set; } = DateTime.UtcNow;
        [Required]
        public int Rating { get; set; }
        public bool IsAvailable { get; set; }


        public string AgentId { get; set; } = null!;
        [ForeignKey("AgentId")]
        public AgentInfo AgentInfo { get; set; } = null!;
        public string? AgentName { get; set; }
        public bool IsActive { get; set; }
    }
}
