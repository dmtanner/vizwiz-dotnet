using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vizwiz.API.Models
{
    public class TagWithoutMessagesDto
    {
        public int Id { get; set; }
        public String Text { get; set; }
        public int NumberMessages { get; set; }
    }
}
