using APIEmpleados.Dtos;
using APIEmpleados.ReglaDeNegocios.Dependencias;
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
    public class DependenciaController : Controller
    {
        private readonly ILogger<DependenciaController> _logger;
        private readonly IDependenciaRepository _idependenciaRepository;

        public DependenciaController(ILogger<DependenciaController> logger, IDependenciaRepository IDependenciaRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _idependenciaRepository = IDependenciaRepository;
        }


        [HttpGet]
        [Route("consultardependencias")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConsultarDependencias()

        {
            try
            {
                var result = await _idependenciaRepository.ConsultarDependencias();
                if (!result.Any())
                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(DependenciaController), nameof(this.ConsultarDependencias), "No se Encontro ninguna Dependencia", result.ToList().Count());
                    return NoContent();
                }

                _logger.LogInformation("{controller}{method} {msg} {result}", nameof(DependenciaController), nameof(this.ConsultarDependencias), "result.count", result.ToList().Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("{controller}{method} {msg} {exception}", nameof(DependenciaController), nameof(this.ConsultarDependencias), "unexpected exception", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("CrearyModificarDependencia")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CrearyModificarDependencia(CrearYModificarDependenciaRequest request)
        {
            try
            {
                if (!ModelState.IsValid)

                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(DependenciaController), nameof(this.CrearyModificarDependencia), "Información Incompleta", request);
                    return BadRequest();
                }

                var result = await _idependenciaRepository.CrearyModificarDependencia(request);

                if (!result.IsSuccessfullProcess)
                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(DependenciaController), nameof(this.CrearyModificarDependencia), result.ErrorMessages, request);
                    return Ok(result.ErrorMessages);
                }

                _logger.LogInformation("{controller}{method} {msg} {result}", nameof(DependenciaController), nameof(this.CrearyModificarDependencia), "dependencia Creado/modificado Exitosamente", request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("{controller}{method} {msg} {exception}", nameof(DependenciaController), nameof(this.CrearyModificarDependencia), "unexpected exception", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("Eliminardependencia")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EliminarDependencia(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(DependenciaController), nameof(this.EliminarDependencia), "Información Incompleta", id);
                    return BadRequest();
                }

                var result = await _idependenciaRepository.EliminarDependencia(id);

                if (!result.IsSuccessfullProcess)
                {
                    _logger.LogInformation("{controller}{method} {msg} {result}", nameof(DependenciaController), nameof(this.EliminarDependencia), result.ErrorMessages, id);
                    return Ok(result.ErrorMessages);
                }

                _logger.LogInformation("{controller}{method} {msg} {result}", nameof(DependenciaController), nameof(this.EliminarDependencia), "Dependencia Eliminada Exitosamente", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("{controller}{method} {msg} {exception}", nameof(DependenciaController), nameof(this.EliminarDependencia), "unexpected exception", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
