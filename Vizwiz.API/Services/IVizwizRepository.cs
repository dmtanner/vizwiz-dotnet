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
        Tag GetTag(int tagId);
        IEnumerable<Message> GetMessagesByTag(int tagId);
        Message GetMessage(int messageId);
        IEnumerable<Message> GetMessages();
        void UpdateTagNumberMessages(int tagId);
        void AddMessage(ICollection<string> tagTexts, Message message);
        void DeleteMessage(Message message);
        bool Save();
    }
}
