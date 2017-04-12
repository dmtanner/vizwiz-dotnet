using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vizwiz.API.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tag needs text")]
        [MaxLength(50, ErrorMessage = "Tag too long")]
        public String Text { get; set; }

        public int NumberMessages { get; set; }

        public ICollection<Message> Messages { get; set; }
            = new List<Message>();

    }
}
