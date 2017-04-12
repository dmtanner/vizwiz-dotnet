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
            var tags = new List<Tag>()
            {
                new Tag()
                {
                    Text = "NeverAgain",
                    NumberMessages = 3,
                    Messages = new List<Message>
                    {
                        new Message()
                        {
                            Text = "why oh why #NeverAgain",
                            PhoneNumber = "1234567899"
                        },
                        new Message()
                        {
                            Text = "me oh my #NeverAgain",
                            PhoneNumber = "2234567899"
                        },
                        new Message()
                        {
                            Text = "this is it #NeverAgain",
                            PhoneNumber = "1234567800"
                        }
                    }
                },
                new Tag()
                {
                    Text = "AlwaysAndForever",
                    NumberMessages = 3,
                    Messages = new List<Message>
                    {
                        new Message()
                        {
                            Text = "yeah baby #AlwaysAndForever",
                            PhoneNumber = "9876543210"
                        },
                        new Message()
                        {
                            Text = "mawwiage #AlwaysAndForever",
                            PhoneNumber = "2534567899"
                        },
                        new Message()
                        {
                            Text = "Kip me outta here #AlwaysAndForever",
                            PhoneNumber = "1234567800"
                        }
                    }
                },
                new Tag()
                {
                    Text = "Word",
                    NumberMessages = 4,
                    Messages = new List<Message>
                    {
                        new Message()
                        {
                            Text = "nothing to say #Word",
                            PhoneNumber = "9876543210"
                        },
                        new Message()
                        {
                            Text = "give me your #Word",
                            PhoneNumber = "2534567899"
                        },
                        new Message()
                        {
                            Text = "outta my head #Word",
                            PhoneNumber = "1234567800"
                        },
                        new Message()
                        {
                            Text = "outta my head2 #Word",
                            PhoneNumber = "1234567800"
                        }
                    }
                }
            };


            context.Tags.AddRange(tags);
            context.SaveChanges();
        }
    }
}
