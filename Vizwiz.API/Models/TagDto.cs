using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vizwiz.API.Models
{
    public class TagDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Text { get; set; }

        public int NumberMessages
        {
            get
            {
                return Messages.Count;
            }
        }

        public ICollection<MessageDto> Messages { get; set; }
        = new List<MessageDto>();

    }
}
