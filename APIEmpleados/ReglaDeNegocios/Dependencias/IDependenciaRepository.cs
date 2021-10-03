using APIEmpleados.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.ReglaDeNegocios.Dependencias
{
    public interface IDependenciaRepository
    {
        Task<IEnumerable<DependenciasDTO>> ConsultarDependencias();

        Task<CrearYModificarDependenciaResponse> CrearyModificarDependencia(CrearYModificarDependenciaRequest request);

        Task<EliminarDependenciaResponse> EliminarDependencia(int id);

    }
}
