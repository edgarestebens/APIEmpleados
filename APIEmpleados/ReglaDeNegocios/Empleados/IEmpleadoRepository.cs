using APIEmpleados.Dtos;
using APIEmpleados.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.ReglaDeNegocios.Empleados
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<EmpleadoDTO>> ConsultarEmpleados();

        Task<CrearyModificarEmpleadoResponse> CrearyModificarEmpleado(CrearyModificarEmpleadorequest request);

        Task<EliminarEmpleadoResponse> EliminarEmpleado(int id);


    }
}
