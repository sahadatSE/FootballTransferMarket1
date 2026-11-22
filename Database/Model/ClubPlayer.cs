
using System;

namespace Database.Model
{
    public class ClubPlayer : BaseModel
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; } = null!;

        public int ClubId { get; set; }
        public Club Club { get; set; } = null!;

        public DateTime JoiningDate { get; set; }

        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }

        public bool IsCurrentClub { get; set; }

        public string Status { get; set; } = "Active";
    }
}
