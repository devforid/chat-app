using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class Message
    {
        public string _id { get; set; }
        public string Sender { get; set; }
        public string SenderId { get; set; }
        public string MessageBody { get; set; }

        public string ThreadId { get; set; }

        public string CreatorId { get; set; }
        public string CreatorUsername { get; set; }
        public string[] ReceipientsId { get; set; }
    }
}
