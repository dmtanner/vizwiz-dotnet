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
        public IActionResult GetTag(int id, bool includeMessages = false)
        {
            var tag = _vizwizRepository.GetTag(id, includeMessages);
            if(tag == null)
            {
                return NotFound();
            }

            // messages requested
            if(includeMessages)
            {
                var tagResult = Mapper.Map<TagDto>(tag);
                return Ok(tagResult);
            }

            // no messages requested
            var result = Mapper.Map<TagWithoutMessagesDto>(tag);
            return Ok(result);

        }
        
        [HttpPost("")]
        public IActionResult CreateTag()
        {
            return NotFound();
        }
    }
}