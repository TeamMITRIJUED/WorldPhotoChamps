namespace Champ.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [DefaultValue(false)]
        public bool IsRead { get; set; }

        public int ContestId { get; set; }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}
