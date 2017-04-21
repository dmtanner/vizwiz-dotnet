using System;
using System.Collections.Generic;
using System.Numerics;

namespace Vizwiz.API.Models
{
    public class MessageDto
    {
        public int Id { get; set; }
        public String Text { get; set; }
        public String PhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public ICollection<TagDto> Tags { get; set; }
            = new List<TagDto>();
    }
}
