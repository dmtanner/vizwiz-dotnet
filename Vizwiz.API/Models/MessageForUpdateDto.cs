﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vizwiz.API.Models
{
    public class MessageForUpdateDto
    {
        [Required(ErrorMessage = "Message needs text")]
        [MaxLength(300, ErrorMessage = "Message too long")]
        public String Text { get; set; }
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public String PhoneNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
