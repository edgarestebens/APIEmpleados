using APIEmpleados.Data;
using APIEmpleados.Dtos;
using APIEmpleados.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.ReglaDeNegocios.Dependencias
{
    public class DependenciaRepository : IDependenciaRepository
    {

        private readonly AplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DependenciaRepository(AplicationDbContext context, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory != null ? loggerFactory.CreateLogger<DependenciaRepository>() : throw new ArgumentNullException(nameof(loggerFactory));
            _mapper = mapper;
        }

        public async Task<IEnumerable<DependenciasDTO>> ConsultarDependencias()
        {
            try
            {
                var empleado = await _context.Dependencias.Select(
                    a => new DependenciasDTO()
                    {
                        Id = a.Id,
                        Nombre = a.Nombre,
                        Empleado = a.Empleado

                    }).ToListAsync();

                _logger.LogInformation("{class}{method} {msg} {result}", nameof(DependenciaRepository), nameof(this.ConsultarDependencias), "result.count", empleado.ToList().Count());
                return empleado;
            }
            catch (Exception ex)
            {

                _logger.LogError("{class}{method} {msg} {exception}", nameof(DependenciaRepository), nameof(this.ConsultarDependencias), "unexpected exception", ex);
                throw;
            }
        }

        public async Task<CrearYModificarDependenciaResponse> CrearyModificarDependencia(CrearYModificarDependenciaRequest request)
        {
            var response = new CrearYModificarDependenciaResponse();

            try
            {

                var dependenciaenbase = _context.Dependencias.AsNoTracking().FirstOrDefault(u => u.Id == request.Id);
                var dependencia = _mapper.Map<Dependencia>(request);
                dependencia.Id = dependenciaenbase != null ? dependenciaenbase.Id : default;
                var result = dependenciaenbase != null ? _context.Dependencias.Update(dependencia) : _context.Dependencias.Add(dependencia);
                await _context.SaveChangesAsync();
                response.IsSuccessfullProcess = true;
                response.DependenciaId = result.Entity.Id;
                _logger.LogInformation("{class}{method} {msg} {result}", nameof(DependenciaRepository), nameof(this.CrearyModificarDependencia), $"dependencia {(dependenciaenbase == null ? "creado" : "modificado")} Exitosamente", request.Id);


            }
            catch (Exception ex)
            {

                _logger.LogError("{class}{method} {msg} {exception}", nameof(DependenciaRepository), nameof(this.CrearyModificarDependencia), "unexpected exception", ex);
            }

            return response;
        }

        public async Task<EliminarDependenciaResponse> EliminarDependencia(int id)
        {
            var response = new EliminarDependenciaResponse();

            try
            {

                var result = _context.Remove(new Dependencia() { Id = id });
                response.IsSuccessfullProcess = true;
                await _context.SaveChangesAsync();
                _logger.LogInformation("{class}{method} {msg} {result}", nameof(DependenciaRepository), nameof(this.EliminarDependencia), "dependencia eliminada exitosamente", id);


            }
            catch (Exception ex)
            {

                _logger.LogError("{class}{method} {msg} {exception}", nameof(DependenciaRepository), nameof(this.EliminarDependencia), "unexpected exception", ex);

            }
            return response;
        }
    }
}
