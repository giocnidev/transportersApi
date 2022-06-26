using Entities.Database;
using Entities.Dtos;
using Entities.Interfaces.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace TransportersApi.Controllers
{
    /// <summary>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController: ControllerBase{
        private readonly ILogger<ContainersController> _logger;
        private readonly IContainerBL _containerBl;

        /// <summary>
        /// </summary>
        public StatsController(
            ILogger<ContainersController> logger,
            IContainerBL containerBl)
        {
            _logger = logger;
            _containerBl = containerBl;
        }

        /// <summary>
        /// Servicio para obtener las estadisticas de los contenedores
        /// despachados, no despachados y el presupuesto utilizado
        /// </summary>
        /// <returns>Return success/fail/error status</returns>
        /// <remarks>
        ///     
        /// **Ejemplo de respuesta 200:**
        ///
        ///     {
        ///           "responseTime": "2022-06-26T11:30:35.1629521-05:00",
        ///           "timeElapsed": "",
        ///           "code": 0,
        ///           "message": "",
        ///           "data": {
        ///               "id": "5a132f06-31e5-4d0f-87d8-71e98a483762",
        ///               "containersDispatched": 46715.86,
        ///               "containersNotDispatched": 8442.18,
        ///               "budgetUsed": 6585.49
        ///           }
        ///     }
        ///     
        /// **Ejemplo de respuesta 500:**
        ///
        ///     {
        ///           "responseTime": "2022-06-26T11:30:35.1629521-05:00",
        ///           "code": 500,
        ///           "message": "Error",
        ///           "data": null
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="500">Server error</response>
        [HttpGet("")]
        public ActionResult GetStats()
        {
            ResponseDto<StatsDto> result = _containerBl.GetStats();
            return StatusCode(result.StatusCode, result);
        }

    }
}
