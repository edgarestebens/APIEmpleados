using APIEmpleados.Dtos;
using APIEmpleados.ReglaDeNegocios.Empleados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : Controller
    {
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IEmpleadoRepository _iempleadoRepository;

        public EmpleadoController(ILogger<EmpleadoController> logger, IEmpleadoRepository IEmpleadoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _iempleadoRepository = IEmpleadoRepository;
        }

        [HttpGet]
        [Route("ConsultarEmpleados")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConsultarEmpleados()

        {
            try
            {
                var result = await _iempleadoRepository.ConsultarEmpleados();
                if (!result.Any())
                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(EmpleadoController), nameof(this.ConsultarEmpleados), "No se Encontro ningun Empleado", result.ToList().Count());
                    return NoContent();
                }

                _logger.LogInformation("{controller}{method} {msg} {result}", nameof(EmpleadoController), nameof(this.ConsultarEmpleados), "result.count", result.ToList().Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("{controller}{method} {msg} {exception}", nameof(EmpleadoController), nameof(this.ConsultarEmpleados), "unexpected exception", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("CrearyModificarEmpleado")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CrearyModificarEmpleado(CrearyModificarEmpleadorequest request)
        {
            try
            {
                if (!ModelState.IsValid)

                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(EmpleadoController), nameof(this.CrearyModificarEmpleado), "Información Incompleta", request);
                    return BadRequest();
                }

                var result = await _iempleadoRepository.CrearyModificarEmpleado(request);

                if (!result.IsSuccessfullProcess)
                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(EmpleadoController), nameof(this.CrearyModificarEmpleado), result.ErrorMessages, request);
                    return Ok(result.ErrorMessages);
                }

                _logger.LogInformation("{controller}{method} {msg} {result}", nameof(EmpleadoController), nameof(this.CrearyModificarEmpleado), "Empleado Creado/modificado Exitosamente",request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("{controller}{method} {msg} {exception}", nameof(EmpleadoController), nameof(this.CrearyModificarEmpleado), "unexpected exception", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete]
        [Route("EliminarEmpleado")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EliminarEmpleado(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(EmpleadoController), nameof(this.EliminarEmpleado), "Información Incompleta", id);
                    return BadRequest();
                }

                var result = await _iempleadoRepository.EliminarEmpleado(id);

                if (!result.IsSuccessfullProcess)
                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(EmpleadoController), nameof(this.EliminarEmpleado), result.ErrorMessages, id);
                    return Ok(result.ErrorMessages);
                }

                _logger.LogInformation("{controller}{method} {msg} {result}", nameof(EmpleadoController), nameof(this.EliminarEmpleado), "Empleado Eliminado Exitosamente", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("{controller}{method} {msg} {exception}", nameof(EmpleadoController), nameof(this.EliminarEmpleado), "unexpected exception", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
