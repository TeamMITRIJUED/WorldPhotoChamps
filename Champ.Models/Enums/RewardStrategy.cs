using System.ComponentModel.DataAnnotations;

namespace Champ.Models.Enums
{
    public enum RewardStrategy
    {
        [Display(Name = "Single winner")]
        SingleWinner,

        [Display(Name = "Top participants")]
        TopParticipants
    }
}
