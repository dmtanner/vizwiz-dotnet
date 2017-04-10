using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vizwiz.API.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        [HttpGet()]
        public IActionResult GetTags()
        {
            return Ok(TagsDataStore.Current.Tags);
        }

        [HttpGet("{id}")]
        public IActionResult GetTag(int id)
        {
            var tag = TagsDataStore.Current.Tags.FirstOrDefault(t => t.Id == id);
            if(tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }
        
        [HttpPost("")]
        public IActionResult CreateTag()
        {
            return NotFound();
        }
    }
}