using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vizwiz.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Vizwiz.API.Services;

namespace Vizwiz.API.Controllers
{
    [Route("api/tags")]
    public class MessagesController : Controller
    {
        private ILogger<MessagesController> _logger;
        private IMailService _mailService;

        public MessagesController(ILogger<MessagesController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet("{tagId}/messages")]
        public IActionResult GetMessages(int tagId)
        {
            try
            {
                var tag = TagsDataStore.Current.Tags.FirstOrDefault(t => t.Id == tagId);
                if(tag == null)
                {
                    _logger.LogInformation($"tag with id {tagId} could not be found while accessing messages");
                    return NotFound();
                }

                return Ok(tag.Messages);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting messages for tag with id {tagId}" + ex);
                return StatusCode(500, "There was an error while handling your request");
            }
        }

        [HttpGet("{tagId}/messages/{messageId}", Name="GetMessage")]
        public IActionResult GetMessage(int tagId, int messageId)
        {
            var tag = TagsDataStore.Current.Tags.FirstOrDefault(t => t.Id == tagId);
            if(tag == null)
            {
                return NotFound();
            }

            var message = tag.Messages.FirstOrDefault(m => m.Id == messageId);
            if(message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [HttpPost("{tagId}/message")]
        public IActionResult CreateMessage(int tagId,
            [FromBody] MessageForCreationDto message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = TagsDataStore.Current.Tags.FirstOrDefault(t => t.Id == tagId);
            if (tag == null)
            {
                return NotFound();
            }

            var maxMessageId = TagsDataStore.Current.Tags.SelectMany(
                t => t.Messages).Max(m => m.Id);

            var finalMessage = new MessageDto()
            {
                Id = maxMessageId + 1,
                Text = message.Text,
                PhoneNumber = message.PhoneNumber
            };

            tag.Messages.Add(finalMessage);

            return CreatedAtRoute("GetMessage", new
            { tagId = tagId, messageId = finalMessage.Id }, finalMessage);
        }

        [HttpPut("{tagId}/messages/{messageId}")]
        public IActionResult UpdateMessage(int tagId, int messageId,
            [FromBody] MessageForUpdateDto message)
        {
            if (message == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = TagsDataStore.Current.Tags.FirstOrDefault(t => t.Id == tagId);
            if (tag == null)
            {
                return NotFound();
            }

            var messageFromStore = tag.Messages.FirstOrDefault(m => m.Id == messageId);
            if(messageFromStore == null)
            {
                return NotFound();
            }

            messageFromStore.Text = message.Text;
            messageFromStore.PhoneNumber = message.PhoneNumber;

            return NoContent();
        }

        [HttpPatch("{tagId}/messages/{messageId}")]
        public IActionResult PartiallyUpdateMessage(int tagId, int messageId,
            [FromBody] JsonPatchDocument<MessageForUpdateDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var tag = TagsDataStore.Current.Tags.FirstOrDefault(t => t.Id == tagId);
            if (tag == null)
            {
                return NotFound();
            }

            var messageFromStore = tag.Messages.FirstOrDefault(m => m.Id == messageId);
            if(messageFromStore == null)
            {
                return NotFound();
            }

            var messageToPatch = new MessageForUpdateDto()
            {
                Text = messageFromStore.Text,
                PhoneNumber = messageFromStore.PhoneNumber
            };

            patchDoc.ApplyTo(messageToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(messageToPatch);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            messageFromStore.Text = messageToPatch.Text;
            messageFromStore.PhoneNumber = messageToPatch.PhoneNumber;

            return NoContent();
        }

        [HttpDelete("{tagId}/messages/{messageId}")]
        public IActionResult DeleteMessage(int tagId, int messageId)
        {
            var tag = TagsDataStore.Current.Tags.FirstOrDefault(t => t.Id == tagId);
            if (tag == null)
            {
                return NotFound();
            }

            var messageFromStore = tag.Messages.FirstOrDefault(m => m.Id == messageId);
            if(messageFromStore == null)
            {
                return NotFound();
            }

            tag.Messages.Remove(messageFromStore);
            _mailService.Send("message deleted", $"{messageId} deleted from database");

            return NoContent();
        }
    }
}