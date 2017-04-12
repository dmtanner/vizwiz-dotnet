using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizwiz.API.Entities;

namespace Vizwiz.API.Services
{
    public class VizwizRepository : IVizwizRepository
    {
        private VizwizContext _vizwizContext;
        public VizwizRepository(VizwizContext context)
        {
            _vizwizContext = context;
        }

        public bool TagExists(int tagId)
        {
            return _vizwizContext.Tags.Any(t => t.Id == tagId);
        }

        public Message GetMessage(int messageId)
        {
            return _vizwizContext.Messages.Where(m => m.Id == messageId)
                .FirstOrDefault();
        }

        public IEnumerable<Message> GetMessagesForTag(int tagId)
        {
            return _vizwizContext.Messages.Where(m => m.TagId == tagId)
                .ToList();
        }

        public Tag GetTag(int tagId, bool includeMessages)
        {
            if(includeMessages)
            {
                return _vizwizContext.Tags.Include(t => t.Messages)
                    .Where(t => t.Id == tagId).FirstOrDefault();
            }

            return _vizwizContext.Tags.Where(t => t.Id == tagId).FirstOrDefault();
        }

        public IEnumerable<Tag> GetTags()
        {
            return _vizwizContext.Tags.OrderBy(t => t.Text).ToList();
        }

        public void UpdateTagNumberMessages(int tagId)
        {
            int messageCount = _vizwizContext.Messages.Where(m => m.TagId == tagId).Count();
            var tag = _vizwizContext.Tags.Where(t => t.Id == tagId).FirstOrDefault();
            tag.NumberMessages = messageCount;
            _vizwizContext.Tags.Update(tag);
        }

        public void AddMessage(int tagId, Message message)
        {
            var tag = GetTag(tagId, false);
            tag.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _vizwizContext.Messages.Remove(message);
        }

        public bool Save()
        {
            return (_vizwizContext.SaveChanges() >= 0);
        }

    }
}
