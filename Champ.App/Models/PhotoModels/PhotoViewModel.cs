using System.ComponentModel.DataAnnotations;

namespace Champ.App.Models.PhotoModels
{
    public class PhotoViewModel
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Author { get; set; }

        public int ContestId { get; set; }
    }
}