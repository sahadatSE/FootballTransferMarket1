using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{

    public class ChatMessage : BaseModel
    {
        [Key]
        [Required]
        public string MessageId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string ChatId { get; set; } = null!;
        [ForeignKey("ChatId")]
        public Chat Chat { get; set; } = null!;

        [Required]
        public string SenderId { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string SenderType { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string MessageText { get; set; } = null!;

        public DateTime SentDate { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}