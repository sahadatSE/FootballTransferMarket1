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
        [Required]  
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

        public string PlayerId { get; set; } = null!;
        [ForeignKey("PlayerId")]
        public string PlayerName { get; set; } = null!;
    
        public  Player Player { get; set; } = null!;
         



    }
}
