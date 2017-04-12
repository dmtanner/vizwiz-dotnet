using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
