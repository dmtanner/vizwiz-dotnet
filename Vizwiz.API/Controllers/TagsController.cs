using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vizwiz.API.Services;
using Vizwiz.API.Models;
using AutoMapper;

namespace Vizwiz.API.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        private IVizwizRepository _vizwizRepository;

        public TagsController(IVizwizRepository repository)
        {
            _vizwizRepository = repository;
        }

        [HttpGet()]
        public IActionResult GetTags()
        {
            var tagEntities = _vizwizRepository.GetTags();
            var results = Mapper.Map<IEnumerable<TagWithoutMessagesDto>>(tagEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetTag(int id)
        {
            var tag = _vizwizRepository.GetTag(id);
            if(tag == null)
            {
                return NotFound();
            }

            // no messages requested
            var result = Mapper.Map<TagWithoutMessagesDto>(tag);
            return Ok(result);

        }

        [HttpGet("{tagId}/messages")]
        public IActionResult GetTagMessages(int tagId)
        {
            var messages = _vizwizRepository.GetMessagesByTag(tagId);
            if(messages == null)
            {
                return BadRequest();
            }

            var messagesResults = Mapper.Map<IEnumerable<MessageDto>>(messages);

            return Ok(messagesResults);
        }
        
        [HttpPost()]
        public IActionResult CreateTag()
        {
            return NotFound();
        }
    }
}