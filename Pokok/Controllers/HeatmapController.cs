using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Pokok.Models;

namespace Pokok.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeatmapController : ControllerBase
    {
        private readonly ILogger<HeatmapController> _logger;

        public HeatmapController(ILogger<HeatmapController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Heatmap> Get()
        {
            return new Heatmap[] {
                    new Heatmap(3.0727, 101.5921, 0.6),
                    new Heatmap(3.0727356, 101.5824709, 0.5),
                    new Heatmap(3.0668511, 101.5908615, 0.7),
                    new Heatmap(3.0768062, 101.5806735, 1)
            };
        }
    }
}

