using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vizwiz.API.Models
{
    public class TagDto
    {
        public int Id { get; set; }
        public String Text { get; set; }
        public int NumberMessages { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
        = new List<MessageDto>();
    }
}
