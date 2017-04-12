using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizwiz.API.Entities;

namespace Vizwiz.API.Controllers
{
    public class DummyController: Controller
    {
        private VizwizContext _ctx;

        public DummyController(VizwizContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
