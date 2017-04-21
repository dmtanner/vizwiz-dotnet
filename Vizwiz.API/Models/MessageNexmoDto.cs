using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Vizwiz.API.Models
{
    [DataContract]
    public class MessageNexmoDto
    {
        // Common params
        [DataMember(Name="msisdn")]
        [Phone(ErrorMessage = "Invalid From Phone Number")]
        public string From { get; set; }

        [DataMember(Name="to")]
        [Phone(ErrorMessage = "Invalid To Phone Number")]
        public string To { get; set; }

        [DataMember(Name="messageId")]
        public string MessageId { get; set; }

        [DataMember(Name="text")]
        [Required(ErrorMessage = "Message needs text")]
        [MaxLength(300, ErrorMessage = "Message too long")]
        public string Text { get; set; }

        [DataMember(Name="type")]
        public string Type { get; set; }

        [DataMember(Name="keyword")]
        public string Keyword { get; set; }

        [DataMember(Name="message-timestamp")]
        [DataType(DataType.DateTime)]
        public string Date { get; set; }


        // Less used params
        [DataMember(Name="timestamp")]
        public string Timestamp { get; set; }

        [DataMember(Name="nonce")]
        public string Nonce { get; set; }

        [DataMember(Name="concat")]
        public string Concat { get; set; }

        [DataMember(Name="concat-ref")]
        public string ConcatRef { get; set; }

        [DataMember(Name="concat-total")]
        public string ConcatTotal { get; set; }

        [DataMember(Name="concat-part")]
        public string ConcatPart { get; set; }

        [DataMember(Name="data")]
        public string Data { get; set; }

        [DataMember(Name="udh")]
        public string Udh { get; set; }

    }
}
