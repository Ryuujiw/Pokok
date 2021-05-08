using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Pokok.Models;
using Pokok.Services;
using Microsoft.Extensions.Configuration;
using System.Drawing;

namespace Pokok.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreeController : ControllerBase
    {
        private readonly ILogger<TreeController> _logger;
        private readonly IConfiguration _configuration;
        public TreeService TreeService { get; set; }

        public TreeController(ILogger<TreeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            TreeService = new TreeService(configuration.GetConnectionString("PokokDB"));
        }

        [HttpGet]
        public IEnumerable<Location> ListLocations()
        {
            return TreeService.GetAllLocations();
        }

        [HttpGet("{id}")]
        public Location GetLocation(string id)
        {
            return new Location(3.0727, 101.5921, 0.6);
        }
    }
}

