using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vizwiz.API.Models
{
    public class TagForCreationDto
    {
        [Required(ErrorMessage = "Tag needs text")]
        [MaxLength(50, ErrorMessage = "Tag too long")]
        public String Text { get; set; }
        [Range(0, 9999999999, ErrorMessage = "Invalid Tag Count")]
        public int NumberMessages { get; set; }
    }
}
