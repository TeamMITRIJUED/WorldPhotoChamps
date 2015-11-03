namespace Champ.App.Models.ContestModels
{
    using System.Collections.Generic;

    public class BrowseContestsViewModel
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public IEnumerable<ContestViewModel> Contests { get; set; } 
    }
}