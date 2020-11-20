using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MessageCore.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public int SenderLocalId { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Receiver { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime SendedTime { get; set; }

        [Required]
        public MessageState State { get; set; }
    }

    [Flags]
    public enum MessageState
    {
        NoOneReceived = 0,
        SenderReceived = 1,
        ReceiverReceived = 2
    }
}
