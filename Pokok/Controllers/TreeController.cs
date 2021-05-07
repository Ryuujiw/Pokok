using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Pokok.Models;

namespace Pokok.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreeController : ControllerBase
    {
        private readonly ILogger<TreeController> _logger;

        public TreeController(ILogger<TreeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Location> ListLocations()
        {
            return new Location[] {
                    new Location(3.0727, 101.5921, 0.6),
                    new Location(3.0727356, 101.5824709, 0.5),
                    new Location(3.0668511, 101.5908615, 0.7),
                    new Location(3.0768062, 101.5806735, 1)
            };
        }

        [HttpGet("{id}")]
        public int GetLocation(string id)
        {
            return 1;
        }
    }
}

