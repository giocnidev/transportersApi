using Entities.Dtos;
using Entities.Interfaces.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace TransportersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContainersController : ControllerBase{
        private readonly ILogger<ContainersController> _logger;
        private readonly IContainerBL _containerBl;

        public ContainersController(
            ILogger<ContainersController> logger,
            IContainerBL containerBl)
        {
            _logger = logger;
            _containerBl = containerBl;
        }

        [HttpPost("")]
        public ActionResult PostContainers(DispatchDto dispatchDto){
            ResponseDto<string?[]> result = _containerBl.SelectContainers(dispatchDto.Budget, dispatchDto.Containers);
            return StatusCode(result.StatusCode, result);
        }
    }
}
