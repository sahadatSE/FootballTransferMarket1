using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Model
{
    public class AgentInfo : BaseModel
    {
        [Key]
        public string AgentId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(50)]
        public String AgentName { get; set; }= null!;
        [Required]
        [StringLength(15)]
        public string AgentNumber { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public String AgentEmail { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public String LicenseNumber { get; set; }   =null!;
        [ForeignKey("Player")]
        public string PlayerId { get; set; } = Guid.NewGuid().ToString();
        public string PlayerName { get; set; }= null!;  



    }
}
