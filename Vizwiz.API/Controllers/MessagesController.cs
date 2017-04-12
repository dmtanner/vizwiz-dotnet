using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vizwiz.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Vizwiz.API.Services;
using AutoMapper;

namespace Vizwiz.API.Controllers
{
    [Route("api/tags")]
    public class MessagesController : Controller
    {
        private ILogger<MessagesController> _logger;
        private IMailService _mailService;
        private IVizwizRepository _vizwizRepository;

        public MessagesController(ILogger<MessagesController> logger, IMailService mailService,
            IVizwizRepository repository)
        {
            _logger = logger;
            _mailService = mailService;
            _vizwizRepository = repository;
        }

        [HttpGet("{tagId}/messages")]
        public IActionResult GetMessages(int tagId)
        {
            try
            {
                if(!_vizwizRepository.TagExists(tagId))
                {
                    _logger.LogInformation($"tag with id {tagId} could not be found while accessing messages");
                    return NotFound();
                }

                var messagesForTag = _vizwizRepository.GetMessagesForTag(tagId);

                var messagesForTagResults = Mapper.Map<IEnumerable<MessageDto>>(messagesForTag); 

                return Ok(messagesForTagResults);
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
            if(!_vizwizRepository.TagExists(tagId))
            {
                return NotFound();
            }

            var message = _vizwizRepository.GetMessage(messageId);
            if(message == null)
            {
                return NotFound();
            }

            var messageResults = Mapper.Map<MessageDto>(message);

            return Ok(messageResults);
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

            if (!_vizwizRepository.TagExists(tagId))
            {
                return NotFound();
            }


            var finalMessage = Mapper.Map<Entities.Message>(message);

            _vizwizRepository.AddMessage(tagId, finalMessage);
            if(!_vizwizRepository.Save())
            {
                return StatusCode(500, "A problem happened while adding message");
            }
            // update the message count for tag
            _vizwizRepository.UpdateTagNumberMessages(tagId);
            if(!_vizwizRepository.Save())
            {
                return StatusCode(500, "A problem happened while adding message");
            }

            var createdMessage = Mapper.Map<MessageDto>(finalMessage);

            return CreatedAtRoute("GetMessage", new
            { tagId = tagId, messageId = createdMessage.Id }, createdMessage);
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

            if (!_vizwizRepository.TagExists(tagId))
            {
                return NotFound();
            }

            var messageEntity = _vizwizRepository.GetMessage(messageId); 
            if(messageEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(message, messageEntity);

            if(!_vizwizRepository.Save())
            {
                return StatusCode(500, "There was a problem while updating your message");
            }

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

            if (!_vizwizRepository.TagExists(tagId))
            {
                return NotFound();
            }

            var messageEntity = _vizwizRepository.GetMessage(messageId); 
            if(messageEntity == null)
            {
                return NotFound();
            }

            var messageToPatch = Mapper.Map<MessageForUpdateDto>(messageEntity);

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

            Mapper.Map(messageToPatch, messageEntity);
            if(!_vizwizRepository.Save())
            {
                return StatusCode(500, "There was a problem while updating your message");
            }

            return NoContent();
        }

        [HttpDelete("{tagId}/messages/{messageId}")]
        public IActionResult DeleteMessage(int tagId, int messageId)
        {
            if (!_vizwizRepository.TagExists(tagId))
            {
                return NotFound();
            }

            var messageEntity = _vizwizRepository.GetMessage(messageId); 
            if(messageEntity == null)
            {
                return NotFound();
            }

            _vizwizRepository.DeleteMessage(messageEntity);
            if(!_vizwizRepository.Save())
            {
                return StatusCode(500, "There was a problem while deleting your message");
            }
            // update the message count for tag
            _vizwizRepository.UpdateTagNumberMessages(tagId);
            if(!_vizwizRepository.Save())
            {
                return StatusCode(500, "A problem happened while adding message");
            }
            
            _mailService.Send("message deleted", $"{messageId} deleted from database");

            return NoContent();
        }
    }
}