using Entities.Database;
using Entities.Dtos;
using Entities.Interfaces.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace TransportersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController: ControllerBase{
        private readonly ILogger<ContainersController> _logger;
        private readonly IContainerBL _containerBl;

        public StatsController(
            ILogger<ContainersController> logger,
            IContainerBL containerBl)
        {
            _logger = logger;
            _containerBl = containerBl;
        }

        [HttpGet("")]
        public ActionResult GetStats()
        {
            ResponseDto<Stats> result = _containerBl.GetStats();
            return StatusCode(result.StatusCode, result);
        }

    }
}
