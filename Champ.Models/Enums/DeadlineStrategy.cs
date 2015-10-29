using System.ComponentModel.DataAnnotations;

namespace Champ.Models.Enums
{
    public enum DeadlineStrategy
    {
        [Display(Name = "By Time")]
        ByTime,

        [Display(Name = "By Participant Number")]
        ByNumber
    }
}
