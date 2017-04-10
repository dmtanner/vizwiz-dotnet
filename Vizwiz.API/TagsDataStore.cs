using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizwiz.API.Models;

namespace Vizwiz.API
{
    public class TagsDataStore
    {
        public static TagsDataStore Current { get; } = new TagsDataStore();
        public List<TagDto> Tags { get; set; }

        public TagsDataStore()
        {
            Tags = new List<TagDto>()
            {
                new TagDto()
                {
                    Id = 1,
                    Text = "NeverAgain",
                    NumberMessages = 47,
                    Messages = new List<MessageDto>
                    {
                        new MessageDto()
                        {
                            Id = 1,
                            Text = "why oh why #NeverAgain",
                            PhoneNumber = "1234567899"
                        },
                        new MessageDto()
                        {
                            Id = 2,
                            Text = "me oh my #NeverAgain",
                            PhoneNumber = "2234567899"
                        },
                        new MessageDto()
                        {
                            Id = 3,
                            Text = "this is it #NeverAgain",
                            PhoneNumber = "1234567800"
                        }
                    }
                },
                new TagDto()
                {
                    Id = 2,
                    Text = "AlwaysAndForever",
                    NumberMessages = 49,
                    Messages = new List<MessageDto>
                    {
                        new MessageDto()
                        {
                            Id = 4,
                            Text = "yeah baby #AlwaysAndForever",
                            PhoneNumber = "9876543210"
                        },
                        new MessageDto()
                        {
                            Id = 5,
                            Text = "mawwiage #AlwaysAndForever",
                            PhoneNumber = "2534567899"
                        },
                        new MessageDto()
                        {
                            Id = 6,
                            Text = "Kip me outta here #AlwaysAndForever",
                            PhoneNumber = "1234567800"
                        }
                    }
                },
                new TagDto()
                {
                    Id = 3,
                    Text = "Word",
                    NumberMessages = 4,
                    Messages = new List<MessageDto>
                    {
                        new MessageDto()
                        {
                            Id = 7,
                            Text = "nothing to say #Word",
                            PhoneNumber = "9876543210"
                        },
                        new MessageDto()
                        {
                            Id = 8,
                            Text = "give me your #Word",
                            PhoneNumber = "2534567899"
                        },
                        new MessageDto()
                        {
                            Id = 9,
                            Text = "outta my head #Word",
                            PhoneNumber = "1234567800"
                        },
                        new MessageDto()
                        {
                            Id = 10,
                            Text = "outta my head2 #Word",
                            PhoneNumber = "1234567800"
                        }
                    }
                }
            };
        }
    }
}
