using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.ViewModel
{
    internal class UserRoleInfo
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        [Required, MaxLength(40)]
        public string? FullName { get; set; }
        [Required ]
        public string? Email { get; set; }
        [Required ]
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public int RoleID { get; set; }
        public string RoleNmae { get; set; }
        public string? UpadatedByName { get; set; }
    }
}
