using System;
using System.ComponentModel.DataAnnotations;

namespace Vizwiz.API.Models
{
    public class MessageForCreationDto
    {
        [Required(ErrorMessage = "Message needs text")]
        [MaxLength(300, ErrorMessage = "Message too long")]
        public String Text { get; set; }
        
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public String PhoneNumber { get; set; }

        public DateTime Date { get; set; }
    }
}
