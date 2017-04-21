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


        public Tag GetTag(int tagId)
        {
            return _vizwizContext.Tags.Where(t => t.Id == tagId).FirstOrDefault();
        }

        public IEnumerable<Tag> GetTags()
        {
            return _vizwizContext.Tags.OrderBy(t => t.Text).ToList();
        }

        public void UpdateTagNumberMessages(int tagId)
        {
            //int messageCount = _vizwizContext.Messages.Where(m => m.TagId == tagId).Count();
            //var tag = _vizwizContext.Tags.Where(t => t.Id == tagId).FirstOrDefault();
            //tag.NumberMessages = messageCount;
            //_vizwizContext.Tags.Update(tag);
        }

        public Message GetMessage(int messageId)
        {
            return _vizwizContext.Messages.Where(m => m.Id == messageId)
                .FirstOrDefault();
        }

        public IEnumerable<Message> GetMessages()
        {
            return _vizwizContext.Messages.OrderBy(m => m.PhoneNumber);
        }

        public IEnumerable<Message> GetMessagesByTag(int tagId)
        {
            return _vizwizContext.Messages.Where(m => m.MessageTags.Any(mt => mt.TagId == tagId))
                .ToList();
        }

        public void AddMessage(ICollection<string> tagTexts, Message message)
        {
            // add message to db
            _vizwizContext.Messages.Add(message);

            // create MessageTags for each tag
            foreach (string tagText in tagTexts)
            {
                Tag tag = _vizwizContext.Tags.Where(t => t.Text == tagText).FirstOrDefault();
                if(tag == null)
                {
                    tag = new Tag()
                    {
                        Text = tagText,
                        NumberMessages = 1
                    };
                    _vizwizContext.Add(tag);
                }

                MessageTag mt = new MessageTag()
                {
                    Message = message,
                    Tag = tag
                };


                tag.MessageTags.Add(mt);
                message.MessageTags.Add(mt);

                //_vizwizContext.Update(tag);
            }

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
