using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vizwiz.API.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Message needs text")]
        [MaxLength(300, ErrorMessage = "Message too long")]
        public String Text { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        public String PhoneNumber { get; set; }

        public DateTime Date { get; set; }

        public  ICollection<MessageTag> MessageTags { get; set; }
            = new List<MessageTag>();

    }
}
