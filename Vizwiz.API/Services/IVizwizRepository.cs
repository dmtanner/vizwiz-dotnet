using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizwiz.API.Entities;

namespace Vizwiz.API.Services
{
    public interface IVizwizRepository
    {
        bool TagExists(int tagId);
        IEnumerable<Tag> GetTags();
        Tag GetTag(int tagId, bool includeMessages);
        IEnumerable<Message> GetMessagesForTag(int tagId);
        Message GetMessage(int messageId);
        void UpdateTagNumberMessages(int tagId);
        void AddMessage(int tagId, Message message);
        void DeleteMessage(Message message);
        bool Save();
    }
}
