using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    public class Chat : BaseModel
    {
        [Key]
        [Required]
        public string ChatId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string PlayerId { get; set; } = null!;
        [ForeignKey("PlayerId")]
        public Player Player { get; set; } = null!;

        [Required]
        public string AgentId { get; set; } = null!;
        [ForeignKey("AgentId")]
        public AgentInfo Agent { get; set; } = null!;

        [Required]
        public string ClientId { get; set; } = null!;
        [ForeignKey("ClientId")]
        public UserInfo Client { get; set; } = null!;

        public DateTime LastMessageDate { get; set; } = DateTime.UtcNow;
    }
}