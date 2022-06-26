using Entities.Dtos;
using Entities.Interfaces.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace TransportersApi.Controllers
{
    /// <summary>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContainersController : ControllerBase{
        private readonly ILogger<ContainersController> _logger;
        private readonly IContainerBL _containerBl;

        /// <summary>
        /// </summary>
        public ContainersController(
            ILogger<ContainersController> logger,
            IContainerBL containerBl)
        {
            _logger = logger;
            _containerBl = containerBl;
        }

        /// <summary>
        /// Servicio para obtener los contenedores que se pueden despachar 
        /// sin sobrepasar el presupuesto suministrado    
        /// </summary>
        /// <param name="dispatchDto">Contiene el presupuesto y el listado de contenedores</param>
        /// <returns>Return success/fail/error status</returns>
        /// <remarks>
        /// **Ejemplo de petición:**
        ///
        ///     {
        ///         "budget": 1508.65,
        ///         "containers": [
        ///             {
        ///                 "name": "c1",
        ///                 "transportCost": 571.40,
        ///                 "containerPrice": 4744.03
        ///             },{
        ///                 "name": "c2",
        ///                 "transportCost": 537.33,
        ///                 "containerPrice": 3579.07
        ///             }
        ///         ]
        ///     }
        ///     
        /// **Ejemplo de respuesta 200:**
        ///
        ///     {
        ///           "responseTime": "2022-06-26T11:30:35.1629521-05:00",
        ///           "timeElapsed": "1025 ms",
        ///           "code": 0,
        ///           "message": "",
        ///           "data": [
        ///               "c1",
        ///               "c2",
        ///               "c4"
        ///           ]
        ///     }
        ///     
        /// **Ejemplo de respuesta 400 - 500:**
        ///
        ///     {
        ///           "responseTime": "2022-06-26T11:30:35.1629521-05:00",
        ///           "code": 101,
        ///           "message": "Se debe proporcionar un presupuesto válido.",
        ///           "data": null
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Failed</response>
        /// <response code="500">Server error</response>
        [HttpPost("")]
        public ActionResult PostContainers(DispatchDto dispatchDto){
            ResponseDto<string?[]> result = _containerBl.SelectContainers(dispatchDto.Budget, dispatchDto.Containers);
            return StatusCode(result.StatusCode, result);
        }
    }
}
