using APIEmpleados.Data;
using APIEmpleados.Dtos;
using APIEmpleados.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.ReglaDeNegocios.Empleados
{
    public class EmpleadoRepository : IEmpleadoRepository
    {

        private readonly AplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public EmpleadoRepository(AplicationDbContext context, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = loggerFactory != null ? loggerFactory.CreateLogger<EmpleadoRepository>() : throw new ArgumentNullException(nameof(loggerFactory));
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmpleadoDTO>> ConsultarEmpleados()
        {
            try
            {
                var empleado = await _context.Empleados.Select(
                    a => new EmpleadoDTO()
                    {
                        Id = a.Id,
                        Nombres = a.Nombres,
                        Apellidos = a.Apellidos,
                        Correo = a.Correo

                    }).ToListAsync();

                _logger.LogInformation("{class}{method} {msg} {result}", nameof(EmpleadoRepository), nameof(this.ConsultarEmpleados), "result.count", empleado.ToList().Count());
                return empleado;
            }
            catch (Exception ex)
            {

                _logger.LogError("{class}{method} {msg} {exception}", nameof(EmpleadoRepository), nameof(this.ConsultarEmpleados), "unexpected exception", ex);
                throw;
            }
        }

      
        public async Task<CrearyModificarEmpleadoResponse> CrearyModificarEmpleado(CrearyModificarEmpleadorequest request)
        {
            var response = new CrearyModificarEmpleadoResponse();

            try
            {

              
                var empleadoenbase = _context.Empleados.AsNoTracking().FirstOrDefault(u => u.Id == request.Id);
                var empleado = _mapper.Map<Empleado>(request);
                empleado.Id = empleadoenbase != null ? empleadoenbase.Id : default;
                var result = empleadoenbase != null ? _context.Empleados.Update(empleado) : _context.Empleados.Add(empleado);
                await _context.SaveChangesAsync();
                response.IsSuccessfullProcess = true;
                response.EmpleadoId = result.Entity.Id;
                _logger.LogInformation("{class}{method} {msg} {result}", nameof(EmpleadoRepository), nameof(this.CrearyModificarEmpleado), $"empleado {(empleadoenbase == null ? "creado" : "modificado")} Exitosamente", request.Id);


            }
            catch (Exception ex)
            {

                _logger.LogError("{class}{method} {msg} {exception}", nameof(EmpleadoRepository), nameof(this.CrearyModificarEmpleado), "unexpected exception", ex);
            }

            return response;
        }

        public async Task<EliminarEmpleadoResponse> EliminarEmpleado(int id)
        {

            var response = new EliminarEmpleadoResponse();

            try
            {

                var result = _context.Remove(new Empleado() { Id = id });
                response.IsSuccessfullProcess = true;
                await _context.SaveChangesAsync();
                _logger.LogInformation("{class}{method} {msg} {result}", nameof(EmpleadoRepository), nameof(this.EliminarEmpleado), "Empleado eliminado exitosamente", id);


            }
            catch (Exception ex)
            {

                _logger.LogError("{class}{method} {msg} {exception}", nameof(EmpleadoRepository), nameof(this.CrearyModificarEmpleado), "unexpected exception", ex);
            }

            return response;
        }
    }
}
