using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizwiz.API.Entities;
using Vizwiz.API.Models;

namespace Vizwiz.API
{
    public static class VizwizContextExtensions
    {
        public static void EnsureSeedDataForContext(this VizwizContext context)
        {
            if(context.Tags.Any())
            {
                return;
            }

            // init seed data

            // Create Tag
            Tag tag = new Tag()
            {
                Text = "Word",
                NumberMessages = 2
            };

            // Create 2 messages
            Message message1 = new Message()
            {
                Text = "nothing to say #Word",
                PhoneNumber = "9876543210",
                Date = DateTime.Parse("2016-07-05 21:46:15")
            };
            Message message2 = new Message()
            {
                Text = "more to say here #Word",
                PhoneNumber = "7776543210",
                Date = DateTime.Parse("2016-07-05 21:46:15")
            };

            // create 2 MessageTags
            MessageTag mt1 = new MessageTag();
            MessageTag mt2 = new MessageTag();

            // map message 1 to tag
            mt1.Message = message1;
            mt1.Tag = tag;

            // map message 2 to tag
            mt2.Message = message2;
            mt2.Tag = tag;

            // link mappings to tag
            tag.MessageTags.Add(mt1);
            tag.MessageTags.Add(mt2);

            // Add tag and 2 messages to db
            context.Tags.Add(tag);
            context.Messages.Add(message1);
            context.Messages.Add(message2);
            context.SaveChanges();
        }
    }
}
