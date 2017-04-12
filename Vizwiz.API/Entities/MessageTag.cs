using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vizwiz.API.Entities
{
    public class MessageTag
    {
        public int MessageId { get; set; }
        public Message Message { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
