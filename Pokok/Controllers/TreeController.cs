using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Pokok.Models;
using Microsoft.AspNetCore.Http;
using Pokok.Resources;
using Pokok.Interfaces;

namespace Pokok.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreeController : ControllerBase
    {
        private readonly ILogger<TreeController> _logger;
        private ITreeService TreeService { get; set; }

        public TreeController(ILogger<TreeController> logger, ITreeService service)
        {
            _logger = logger;
            TreeService = service;
        }

        [HttpGet]
        public IEnumerable<Location> ListLocations()
        {
            return TreeService.GetAllLocations();
        }

        [HttpGet("{id}")]
        public Location GetLocation(Guid id)
        {
            return TreeService.GetLocationById(id);
        }

        [HttpPost]
        public ActionResult<Tree> CreateTree([FromBody]TreeResource resource)
        {
            try
            {
                Tree tree = new Tree(resource.latitude, resource.longitude, resource.species);
                if ( tree == null) return BadRequest();
                TreeService.CreateTree(tree);
                return CreatedAtAction(nameof(Tree), new { id = tree.Id }, tree);
            }
            catch (Exception e)
            {
                // TODO: Log exception somewhere
                return StatusCode(StatusCodes.Status500InternalServerError, "Error when adding this new tree");
            }
        }
    }
}

