using System;
using System.Collections.Generic;
using System.Text;

namespace MessageCore.DTO
{
    public class MessageSyncInfo
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public DateTime NewDateTime { get; set; }

        public MessageSyncInfo(int id, int localId, DateTime newDateTime)
        {
            Id = id;
            LocalId = localId;
            NewDateTime = newDateTime;
        }
    }
}
