
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    public class PlayerAgent : BaseModel
    {
        [Required]
        public int PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; }

        [Required]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public virtual AgentInfo Agent { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;

        public DateTime? ApprovalDate { get; set; }

        public DateTime? EndDate { get; set; }

        // Pending, Accepted, Rejected, Terminated
        [Required]
        public string Status { get; set; } = "Pending";
    }
}
