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
    [Route("api/[controller]")]
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

        [HttpGet()]
        public IActionResult GetMessages()
        {
            try
            {
                var messages = _vizwizRepository.GetMessages();

                var messagesResults = Mapper.Map<IEnumerable<MessageDto>>(messages); 

                return Ok(messagesResults);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting messages" + ex);
                return StatusCode(500, "There was an error while handling your request");
            }
        }

        [HttpGet("{messageId}", Name="GetMessage")]
        public IActionResult GetMessage(int messageId)
        {
            var message = _vizwizRepository.GetMessage(messageId);
            if(message == null)
            {
                return NotFound();
            }

            var messageResults = Mapper.Map<MessageDto>(message);

            return Ok(messageResults);
        }

        [HttpPost()]
        public IActionResult CreateMessage(
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

            var finalMessage = Mapper.Map<Entities.Message>(message);

            ICollection<string> tags = extractTags(finalMessage.Text);

            _vizwizRepository.AddMessage(tags, finalMessage);
            if(!_vizwizRepository.Save())
            {
                return StatusCode(500, "A problem happened while adding message");
            }
            // update the message count for tag
            //_vizwizRepository.UpdateTagNumberMessages(tagId);
            //if(!_vizwizRepository.Save())
            //{
            //    return StatusCode(500, "A problem happened while adding message");
            //}

            var createdMessage = Mapper.Map<MessageDto>(finalMessage);

            return CreatedAtRoute("GetMessage", new
            { messageId = createdMessage.Id }, createdMessage);
        }

        [HttpPut("{messageId}")]
        public IActionResult UpdateMessage(int messageId,
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

        [HttpPatch("{messageId}")]
        public IActionResult PartiallyUpdateMessage(int messageId,
            [FromBody] JsonPatchDocument<MessageForUpdateDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
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

        [HttpDelete("{messageId}")]
        public IActionResult DeleteMessage(int messageId)
        {

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
            //_vizwizRepository.UpdateTagNumberMessages(tagId);
            //if(!_vizwizRepository.Save())
            //{
            //    return StatusCode(500, "A problem happened while adding message");
            //}
            
            _mailService.Send("message deleted", $"{messageId} deleted from database");

            return NoContent();
        }

        private ICollection<string> extractTags(string messageText)
        {
            ICollection<string> tags = new List<string>();
            tags.Add("Poopy");
            tags.Add("Peepy");
            return tags;
        }
    }
}