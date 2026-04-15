using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class UserInfo : BaseModel
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        [Required, MaxLength(40)]
        public string FullName  { get; set; }=null!;
        [Required]
        public string UserEmail { get; set; }=null!;
        [Required]
        public string  PasswordHash { get; set; }=null!;    
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
    }
}
