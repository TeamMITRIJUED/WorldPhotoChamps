namespace Champ.App.Models.HomeModels
{
    using System.Collections.Generic;
    using ContestModels;

    public class HomeViewModel
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public IEnumerable<ContestParticipantViewModel> Contests { get; set; } 
    }
}